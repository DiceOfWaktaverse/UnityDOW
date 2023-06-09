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
            instance.AddManager(typeof(CardManager), CardManager.Instance, false);
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
            GetDesignData(data, "chapter");
            GetDesignData(data, "stage");
            GetDesignData(data, "tag_base");
            GetDesignData(data, "card_pack");
            GetDesignData(data, "card_base");
            GetDesignData(data, "leveling_base");
            GetDesignData(data, "skill_base");
            GetDesignData(data, "char_base");
            GetDesignData(data, "enemy_base");
            GetDesignData(data, "field_base");
            GetDesignData(data, "instant_base");
            GetDesignData(data, "item_base");

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
                var data = it.Current.Value;
                if (name == "" || data.Count < 1)
                    continue;

                switch (name)
                {
                    case "stage":
                        TableManager.GetTable<StageTable>().SetTable(data);
                        break;
                    case "chapter":
                        TableManager.GetTable<ChapterTable>().SetTable(data);
                        break;
                    case "tag_base":
                        TableManager.GetTable<TagTable>().SetTable(data);
                        break;
                    case "card_pack":
                        TableManager.GetTable<CardPackTable>().SetTable(data);
                        break;
                    case "card_base":
                        TableManager.GetTable<CardTable>().SetTable(data);
                        break;
                    case "leveling_base":
                        TableManager.GetTable<LevelingTable>().SetTable(data);
                        break;
                    case "skill_base":
                        TableManager.GetTable<SkillTable>().SetTable(data);
                        break;
                    case "char_base":
                        TableManager.GetTable<CharacterCardTable>().SetTable(data);
                        break;
                    case "field_base":
                        TableManager.GetTable<FieldCardTable>().SetTable(data);
                        break;
                    case "instant_base":
                        TableManager.GetTable<InstantCardTable>().SetTable(data);
                        break;
                    case "item_base":
                        TableManager.GetTable<ItemCardTable>().SetTable(data);
                        break;
                    case "enemy_base":
                        TableManager.GetTable<EnemyTable>().SetTable(data);
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