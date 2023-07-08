using System.Collections.Generic;
using System.Linq;

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

        public string Card { get; protected set; } = "";
        public List<string> Pattern { get; protected set; } = new List<string>();

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
                    case "CARD_KEY":
                        Card = it.Current.Value;
                        break;
                    case "PATTERN":
                        if (it.Current.Value == "")
                            break;
                        Pattern.AddRange(it.Current.Value.Split('|').ToList());
                        break;
                }
            }
        }
    }
}
