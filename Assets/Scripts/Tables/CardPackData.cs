using System.Collections.Generic;

namespace DOW
{
    public class CardPackData : TableData
    {
        private static CardPackTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>CardPackData</returns>
        public static CardPackData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<CardPackTable>();

            return table.Get(key);
        }

        public CardPackData(Dictionary<string, string> data) : base(data) { }

        public string Label { get; protected set; } = "";

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
                }
            }
        }
    }
}
