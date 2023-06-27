using System;
using System.Collections.Generic;

namespace DOW
{

    public enum eSkillType {
        NONE,
        DICE, // 주사위 스킬
        CONDITION, // 조건별로 연계가 되는 스킬
        MAX
    }

    public class SkillData : TableData
    {
        private static SkillTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>SkillData</returns>
        public static SkillData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<SkillTable>();

            return table.Get(key);
        }

        public SkillData(Dictionary<string, string> data) : base(data) { }

        public string Label { get; protected set; } = "";
        public List<eSkillType> Type { get; protected set; } = new List<eSkillType>();
        public string TriggerDescription { get; protected set; } = "";
        public string SummonDescription { get; protected set; } = "";
        public string Trigger { get; protected set; } = "";
        public string Summon { get; protected set; } = "";

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
                    case "LABEL":
                        Label = it.Current.Value;
                        break;
                    case "TYPE":
                        if (it.Current.Value == "")
                            break;
                        string[] fragmant = it.Current.Value.Split('|');
                        for (int i = 0; i < fragmant.Length; ++i)
                        {
                            eSkillType type;
                            if (Enum.TryParse<eSkillType>(fragmant[i], out type))
                                Type.Add(type);
                        }
                        break;
                    case "TRIGGER_DESCRIPTION":
                        TriggerDescription = it.Current.Value;
                        break;
                    case "SUMMON_DESCRIPTION":
                        SummonDescription = it.Current.Value;
                        break;
                    case "TRIGGER":
                        Trigger = it.Current.Value;
                        break;
                    case "SUMMON":
                        Summon = it.Current.Value;
                        break;
                }
            }
        }
    }
}
