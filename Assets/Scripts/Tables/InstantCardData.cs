using System.Collections.Generic;

namespace DOW
{

    public class InstantCardData : TableData
    {
        private static InstantCardTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>InstantCardData</returns>
        public static InstantCardData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<InstantCardTable>();

            return table.Get(key);
        }

        public InstantCardData(Dictionary<string, string> data) : base(data) { }

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
                }
            }
        }
    }
}
