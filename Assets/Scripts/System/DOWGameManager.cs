using System;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class DOWGameManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitPlayMode()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        public static string ClientVersion { get { return "1.0.0"; } }
        private static DOWGameManager instance = null;
        public static DOWGameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DOWGameManager();

                return instance;
            }
        }
        private Dictionary<Type, IManagerBase> managers = null;
        private List<IManagerBase> updateManagers = null;
        private Dictionary<string, bool> isLoadeds = null;

        public bool Isinit { get; private set; } = false;
        public float TimeScale { get; private set; } = 1f;
        public float DTime { get { return Time.deltaTime * TimeScale; } }

        public MonoBehaviour GameObject { get => Game.Instance; }

        public GamePreferenceData GamePrefData { get; private set; } = null;

        public UserInfo User;

        public void Init()
        {
            if (GamePrefData == null)
                GamePrefData = new GamePreferenceData();

            if (managers == null)
                managers = new Dictionary<Type, IManagerBase>();
            else
                managers.Clear();

            if (updateManagers == null)
                updateManagers = new List<IManagerBase>();
            else
                updateManagers.Clear();

            if (isLoadeds == null)
                isLoadeds = new Dictionary<string, bool>();
            else
                isLoadeds.Clear();

            #region Manager 세팅
            instance.AddManager(typeof(TimeManager), TimeManager.Instance, true);
            Instance.AddManager(typeof(SoundManager), SoundManager.Instance, false);
            instance.AddManager(typeof(TableManager), TableManager.Instance, false);
            instance.AddManager(typeof(PopupManager), PopupManager.Instance, false);
            instance.AddManager(typeof(UIManager), UIManager.Instance, false);
            instance.AddManager(typeof(VolumeManager), VolumeManager.Instance, false);
            #endregion

            // initialize UserInfo
            User = new UserInfo();
            User.Initialize();

            SetDesignData();//디자인 데이터 로드
                            //GameDataLoading();

        }

        private Dictionary<string, List<Dictionary<string, string>>> GetData()//데이터 읽어옵시다.
        {
            var data = new Dictionary<string, List<Dictionary<string, string>>>();
            //테이블 구조의 한글은 별로 효율 면에서는 안좋지만, 편의성을 위해서 채택됨.
            GetDesignData(data, "캐릭터카드");
            GetDesignData(data, "스킬카드");
            GetDesignData(data, "Chapter");
            GetDesignData(data, "Stage");

            return data;
        }
        private void GetDesignData(Dictionary<string, List<Dictionary<string, string>>> result, string tableName)
        {
            result.Add(tableName, CSVReader.ReadWithUniqueKeyForFile(eResourcePath.DATA.GetPath(tableName)));
        }
        private void SetDesignData()//데이터 로드합시다.
        {
            var jsonData = GetData();
            if (jsonData == null)
                return;

            var it = jsonData.GetEnumerator();
            while (it.MoveNext())
            {
                var name = it.Current.Key;
                var datas = it.Current.Value;
                if (name == "" || datas.Count < 1)
                    continue;

                switch (name)
                {
                    case "스킬카드":
                        TableManager.GetTable<SkillCardTable>().SetTable(datas);
                        break;
                    case "Stage":
                        TableManager.GetTable<StageTable>().SetTable(datas);
                        break;
                    case "Chapter":
                        TableManager.GetTable<ChapterTable>().SetTable(datas);
                        break;
                    default:
                        break;
                }
            }
            Isinit = true;
        }

        public bool IsLoaded(string key)
        {
            if (isLoadeds == null || !isLoadeds.ContainsKey(key))
                return false;

            return isLoadeds[key];
        }
        public void Update(float dt)
        {
            if (updateManagers == null)
                return;

            var count = updateManagers.Count;
            for (var i = 0; i < count; ++i)
            {
                updateManagers[i]?.Update(dt * TimeScale);
            }
        }

        public bool AddManager(Type type, IManagerBase target, bool isUpdate = false)
        {
            if (managers == null || updateManagers == null)
                return false;

            if (managers.ContainsKey(type)) //매니저 중복
                return false;

            target.Initialize();

            managers.Add(type, target);
            if (isUpdate)
                updateManagers.Add(target);

            return true;
        }

        public bool ContainManager(Type type)
        {
            return managers.ContainsKey(type);
        }

        public bool DelManager(Type type, IManagerBase target)
        {
            if (managers == null || updateManagers == null)
                return false;

            bool isRemove = managers.Remove(type);
            if (isRemove) //매니저 중복
                updateManagers.Remove(target);

            return isRemove;
        }

        public static T GetManager<T>() where T : class, IManagerBase
        {
            var type = typeof(T);

            if (Instance.managers.ContainsKey(type) && Instance.managers[type] is T)
                return Instance.managers[type] as T;

            return null;
        }

        public void SetResolution()
        {
            int setWidth = 1280;
            int setHeight = 720;

            int deviceWidth = Screen.width;
            int deviceHeight = Screen.height;

            Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);
            if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
            {
                float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
                Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
            }
            else // 게임의 해상도 비가 더 큰 경우
            {
                float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
                Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
            }
        }
        public void SetCamera(Camera camera)
        {
            if (camera == null)
                return;

            int setWidth = 1280;
            int setHeight = 720;
            int deviceWidth = Screen.width;
            int deviceHeight = Screen.height;

            if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
            {
                float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
                camera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
            }
            else // 게임의 해상도 비가 더 큰 경우
            {
                float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
                camera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
            }
        }

        public SystemLanguage ConvertSystemLangByStringLang(string _stringLanguage)
        {
            switch (_stringLanguage)
            {
                case "kor":
                case "KOR":
                case "ko-KR":
                    return SystemLanguage.Korean;
                case "ENG":
                case "eng":
                    return SystemLanguage.English;
                case "JPN":
                case "jpn":
                    return SystemLanguage.Japanese;
                default:
                    return SystemLanguage.English;
            }
        }

        public string ConvertStringLangBySystemLang(bool _isUpper = false)
        {
            string stringLang;
            switch (GamePrefData.GameLanguage)
            {
                case SystemLanguage.Korean:
                    stringLang = "kor";
                    break;
                case SystemLanguage.English:
                    stringLang = "eng";
                    break;
                case SystemLanguage.Japanese:
                    stringLang = "jpn";
                    break;
                default:
                    stringLang = "eng";
                    break;
            }

            if (_isUpper)
                stringLang = stringLang.ToUpper();
            return stringLang;
        }
    }
}