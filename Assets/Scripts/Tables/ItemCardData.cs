using System.Collections.Generic;

namespace DOW
{

    public class ItemCardData : TableData
    {
        private static ItemCardTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>ItemCardData</returns>
        public static ItemCardData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<ItemCardTable>();

            return table.Get(key);
        }

        public ItemCardData(Dictionary<string, string> data) : base(data) { }

        public List<string> Skils { get; protected set; } = new List<string>();
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
                    default:
                        if (it.Current.Key.Contains("SKILL") && it.Current.Value != "")
                            Skils.Add(it.Current.Value);
                        break;
                }
            }
        }
    }
}
