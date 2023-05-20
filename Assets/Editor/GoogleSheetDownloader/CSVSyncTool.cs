using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class CSVSyncTool : EditorWindow
{
    private readonly string assetPath = "Assets/Editor/GoogleSheetDownloader/GoogleSheetDownloaderSettings.asset";
    private readonly string csvResourceFolder = "Assets/Resources/Data";
    private readonly string credPath = "Assets/Editor/Token";

    private string docID;
    private List<string> sheetNameList;

    private SheetsService _service;
    private SheetsService service
    {
        get
        {
            // credential 초기화

            if (_service == null)
            {
                UserCredential credential;
                string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
                string ApplicationName = "test app";

                using (var stream = new FileStream("Assets/Editor/GoogleSheetDownloader/credentials.json", FileMode.Open, FileAccess.Read))
                {
                    CancellationTokenSource s_cts = new CancellationTokenSource();
                    s_cts.CancelAfter(20000);

                    try
                    {
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.FromStream(stream).Secrets,
                            Scopes,
                            "user",
                            s_cts.Token,
                            new FileDataStore(credPath, true)).Result;
                    }
                    catch (AggregateException)
                    {
                        Debug.LogWarning("구글 로그인 실패 (20초 경과)");
                        return null;
                    }
                }

                // Create Google Sheets API service.
                _service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                return _service;
            }
            else
            {
                return _service;
            }
        }
    }

    struct CSVFileUploadData
    {
        public FileInfo fileInfo;
        public string name;
        public bool diffServer;
    }

    string path = "Resources/Data";

    CSVFileUploadData[] datas;

    [MenuItem("Custom/CSV Sync Tool")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof(CSVSyncTool));
        window.minSize = new Vector2(460, 500);
        window.Show();
        (window as CSVSyncTool).Init();
    }

    public static void AutoSheetSync()
    {
        CSVSyncTool instance = new CSVSyncTool();
        instance.Init();
        instance.OnDownloadSheets();
    }

    private void Init()
    {
        var asset = AssetDatabase.LoadAssetAtPath<GoogleSheetData>(assetPath);
        if (asset != null)
        {
            docID = asset.DocumentID;
            sheetNameList = asset.SheetName.ToList();
        }

        if (datas == null)
            LoadCSVFileInfo();
    }

    void LoadCSVFileInfo()
    {
        List<CSVFileUploadData> dataList = new List<CSVFileUploadData>();
        var rootPath = new StringBuilder().AppendFormat("{0}/{1}", Application.dataPath, path).ToString();
        var directoryInfo = new DirectoryInfo(rootPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        string[] splitStr = { "." };
        foreach (var fi in fileInfos)
        {
            var extension = fi.Name.Substring(fi.Name.Length - 3);
            if (extension.Equals("csv"))
            {
                var data = new CSVFileUploadData();
                data.fileInfo = fi;
                data.name = fi.Name.Split('.')[0];
                data.diffServer = true;
                dataList.Add(data);
                //Debug.Log($"[{fi.Name}]");
            }
        }

        datas = new CSVFileUploadData[sheetNameList.Count];

        for (int i = 0; i < sheetNameList.Count; i++)
        {
            for (int j = 0; j < dataList.Count; j++)
            {
                if (sheetNameList[i] == dataList[j].name)
                {
                    datas[i] = dataList[j];
                }
            }
        }
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        if (datas == null)
        {
            LoadCSVFileInfo();
        }

        GUILayout.Label("DocumentID", EditorStyles.boldLabel);
        docID = EditorGUILayout.TextField(docID);

        if (sheetNameList != null)
        {
            for (int i = 0; i < sheetNameList.Count; ++i)
            {
                GUILayout.BeginHorizontal();

                sheetNameList[i] = EditorGUILayout.TextField(sheetNameList[i], GUILayout.Width(200));

                if (GUILayout.Button("↓", GUILayout.Width(30)))
                {
                    SaveCurrentSettings();
                    DownloadSheet(sheetNameList[i]);
                    //FileMD5Check();
                }
                
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("설정값 저장"))
        {
            SaveCurrentSettings();
        }
        if (GUILayout.Button("시트 추가"))
        {
            if (sheetNameList == null) sheetNameList = new List<string>();
            sheetNameList.Add(string.Empty);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("시트 일괄 다운로드"))
        {
            SaveCurrentSettings();
            OnClickDownloadButton();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("기록(캐시) 삭제"))
        {
            Directory.Delete(credPath, true);
            File.Delete(credPath + ".meta");
        }

        this.Repaint();
    }

    private void OnClickDownloadButton()
    {
        OnDownloadSheets();
    }

    private void SaveCurrentSettings()
    {
        var data = CreateInstance<GoogleSheetData>();
        data.DocumentID = docID;
        data.SheetName = sheetNameList.ToArray();

        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();
    }

    private void DownloadSheet(string sheetName, bool refreshInfo = true)
    {
        try
        {
            //Debug.Log($"시트 다운로드 시도 : {sheetName}");

            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(docID, sheetName);
            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;
            string csvString = string.Empty;

            int dataCount = 0;
            foreach (var row in values)
            {
                dataCount = Mathf.Max(row.Count, dataCount);
            }

            foreach (var row in values)
            {
                for (int j = 0; j < dataCount; ++j)
                {
                    if (row.Count > j)
                    {
                        if(row[j].ToString().IndexOf(',') == -1)
                        {
                            csvString += row[j].ToString() + ",";
                        } 
                        else
                        {
                            csvString += "\"" + row[j].ToString() + "\",";
                        }
                    }
                    else
                    {
                        csvString += ",";
                    }
                }
                csvString = csvString.Substring(0, csvString.Length - 1);
                csvString += '\n';
            }

            if (values.Count > 0)
            {
                File.WriteAllText(csvResourceFolder + $"/{sheetName}.csv", FileReader.Encrypt(csvString));
            }

            //Debug.Log($"시트 다운로드 완료 : {sheetName}");

            if (refreshInfo)
                LoadCSVFileInfo();
        }
        catch (GoogleApiException e)
        {
            Debug.LogError($"시트 이름 확인 필요 : {sheetName} => {e.Message}");
            EditorUtility.ClearProgressBar();
        }
        catch (Exception)
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private void OnDownloadSheets()
    {
        int downloadedFileCount = 0;
        Debug.Log($"다운로드 시작");
        EditorUtility.DisplayProgressBar("구글 CSV 다운로드", "구글 CSV 다운로드", 0f);
        for (int i = 0; i < sheetNameList.Count; ++i)
        {
            var sheetName = sheetNameList[i];

            EditorUtility.DisplayProgressBar("구글 CSV 다운로드", sheetName, (float)(i + 1) / sheetNameList.Count);
            DownloadSheet(sheetName, false);
            ++downloadedFileCount;
        }
        EditorUtility.ClearProgressBar();

        if (downloadedFileCount > 0)
        {
            Debug.Log($"시트 동기화 완료 : {downloadedFileCount}개 파일");
        }
        else
        {
            Debug.LogWarning("다운로드 된 파일 없음, 설정 확인");
        }

        LoadCSVFileInfo();
    }
}
