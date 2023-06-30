using System.Collections.Generic;
using System.Globalization;

namespace DOW
{

    public class EnemyData : TableData
    {
        private static EnemyTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>EnemyData</returns>
        public static EnemyData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<EnemyTable>();

            return table.Get(key);
        }

        public EnemyData(Dictionary<string, string> data) : base(data) { }

        public string Enemy { get; protected set; } = "";
        public string Label { get; protected set; } = "";
        public float Hp { get; protected set; } = 0f;
        public float Damage { get; protected set; } = 0f;
        public float Recovery { get; protected set; } = 0f;
        public float Defence { get; protected set; } = 0f;
        public float SkillFactor { get; protected set; } = 0f;
        public float EffectFactor { get; protected set; } = 0f;
        public float RecoveryFactor { get; protected set; } = 0f;
        public float DefenceFactor { get; protected set; } = 0f;
        public List<int> Pattern { get; protected set; } = new List<int>();
        public List<string> Skills { get; protected set; } = new List<string>();

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
                    case "ENEMY_KEY":
                        Enemy = it.Current.Value;
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
                    case "EFFECT_FACTOR":
                        EffectFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "RECOVERY_FACTOR":
                        RecoveryFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "DEFENCE_FACTOR":
                        DefenceFactor = float.Parse(it.Current.Value, CultureInfo.InvariantCulture);
                        break;
                    case "PATTERN":
                        if (it.Current.Value == "")
                            break;
                        string[] fragmant = it.Current.Value.Split('|');
                        for (int i = 0; i < fragmant.Length; i++)
                        {
                            Pattern.Add(int.Parse(fragmant[i]) - 1);
                        }
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
