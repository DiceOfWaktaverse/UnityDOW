using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;

namespace DOW
{
    public enum eLevelingType
    {
        NONE = 0,
        NORMAL, // 다음 레벨로 레벨업함
        ADD_STAT, // 현재 레벨 유지 하면서 레벨링 스킬을 1회 실행함
        MAX_LEVEL, // 레벨업을 할 수 없음
        MAX
    }

    public class LevelingData : TableData
    {
        private static LevelingTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>LevelingData</returns>
        public static LevelingData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<LevelingTable>();

            return table.Get(key);
        }

        public LevelingData(Dictionary<string, string> data) : base(data) { }

        public string Card { get; protected set; } = "";
        public string Level { get; protected set; } = "1";
        public eLevelingType LevelingType { get; protected set; } = eLevelingType.NONE;
        public string LevelingDescription { get; protected set; } = "";
        public float Hp { get; protected set; } = 0f;
        public float Damage { get; protected set; } = 0f;
        public float Recovery { get; protected set; } = 0f;
        public float Defence { get; protected set; } = 0f;
        public float SkillFactor { get; protected set; } = 0f;
        public float EffectFactor { get; protected set; } = 0f;
        public float RecoveryFactor { get; protected set; } = 0f;
        public float DefenceFactor { get; protected set; } = 0f;
        public List<string> Skills { get; protected set; } = new List<string>();
        public override void Init()
        {
            base.Init();
        }

        public override void SetData(Dictionary<string, string> data)
        {
            base.SetData(data);

            var it = data.GetEnumerator();

            while (it.MoveNext())
            {
                switch (it.Current.Key)
                {
                    case "KEY"://상위에서 UniqueKeyName으로 동작중.
                        // ignoring last line that is empty in csv file
                        if (it.Current.Value == "")
                            return;
                        break;
                    case "CARD_KEY":
                        Card = it.Current.Value;
                        break;
                    case "LEVEL":
                        Level = it.Current.Value;
                        break;
                    case "LEVEL_TYPE":
                        eLevelingType type;
                        if (Enum.TryParse<eLevelingType>(it.Current.Value, out type))
                            LevelingType = type;
                        else
                            LevelingType = eLevelingType.NONE;
                        break;
                    case "LEVELING_DESC":
                        LevelingDescription = it.Current.Value;
                        break;
                    case "HP_BASE":
                        Hp = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "DAMAGE":
                        Damage = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "RECOVERY":
                        Recovery = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "DEFENCE":
                        Defence = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "SKILL_FACTOR":
                        SkillFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "RECOVERY_FACTOR":
                        RecoveryFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "EFFECT_FACTOR":
                        EffectFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "DEFENCE_FACTOR":
                        DefenceFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    default:
                        if (it.Current.Key.Contains("SKILL") && it.Current.Value != "")
                        {
                            Skills.Add(it.Current.Value);
                        }
                        break;
                }
            }
        }
    }
}
