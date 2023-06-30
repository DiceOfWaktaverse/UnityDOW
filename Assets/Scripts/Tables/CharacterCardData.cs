using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DOW
{

    public class CharacterCardData : TableData
    {
        private static CharacterCardTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>CharacterCardData</returns>
        public static CharacterCardData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<CharacterCardTable>();

            return table.Get(key);
        }

        public CharacterCardData(Dictionary<string, string> data) : base(data) { }

        public string Character { get; protected set; } = "";
        public string Level { get; protected set; } = "1";
        public float Hp { get; protected set; } = 0f;
        public float Damage { get; protected set; } = 0f;
        public float Recovery { get; protected set; } = 0f;
        public float Defence { get; protected set; } = 0f;
        public float SkillFactor { get; protected set; } = 0f;
        public float RecoveryFactor { get; protected set; } = 0f;
        public float DefenceFactor { get; protected set; } = 0f;
        public string LevelingDescription { get; protected set; } = "";
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
                    case "CHAR_KEY":
                        Character = it.Current.Value;
                        break;
                    case "LEVEL":
                        Level = it.Current.Value;
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
                    case "DEFENCE_FACTOR":
                        DefenceFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "LEVELING_DESC":
                        LevelingDescription = it.Current.Value;
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
