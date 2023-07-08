using System;
using System.Collections.Generic;

namespace DOW
{
    public enum eStageType
    {
        NONE,
        NORMAL,
        ELITE,
        BOSS,
        EVENT,
        MAX
    }

    public class StageData : TableData
    {
        private static StageTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>SkillCardData</returns>
        public static StageData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<StageTable>();

            return table.Get(key);
        }

        public StageData(Dictionary<string, string> data) : base(data) { }

        public string Stage { get; protected set; } = "";
        public string Chapter {get; protected set;} = "";
        public string Label { get; protected set; } = "";
        public eStageType Type { get; protected set; } = eStageType.NONE;
        public string CompensationA { get; protected set; } = "";
        public string CompensationB { get; protected set; } = "";
        public string CompensationC { get; protected set; } = "";
        public string EnemyFieldCard { get; protected set; } = "";
        public List<string> Enemy { get; protected set; } = new List<string>();

        public override void Init()
        {
            base.Init();
        }

        public override void SetData(Dictionary<string, string> datas)
        {
            base.SetData(datas);

            var it = datas.GetEnumerator();

            while (it.MoveNext())
            {
                switch (it.Current.Key)
                {
                    case "KEY"://상위에서 UniqueKeyName으로 동작중.
                        // ignoring last line that is empty in csv file
                        if (it.Current.Value == "")
                            return;
                        break;
                    case "CHAPTER_KEY":
                        Chapter = it.Current.Value;
                        break;
                    case "LABEL":
                        Label = it.Current.Value;
                        break;
                    case "TYPE":
                        eStageType type;
                        if (Enum.TryParse<eStageType>(it.Current.Value, out type))
                            Type = type;
                        else
                            Type = eStageType.NONE;
                        break;
                    case "COMPENSATION_A":
                        CompensationA = it.Current.Value;
                        break;
                    case "COMPENSATION_B":
                        CompensationB = it.Current.Value;
                        break;
                    case "COMPENSATION_C":
                        CompensationC = it.Current.Value;
                        break;
                    case "ENEMY_FIELD_CARD":
                        EnemyFieldCard = it.Current.Value;
                        break;
                    default: 
                        if (it.Current.Key.Contains("ENEMY"))
                            Enemy.Add(it.Current.Value);
                        break;
                }
            }
        }
    }
}
