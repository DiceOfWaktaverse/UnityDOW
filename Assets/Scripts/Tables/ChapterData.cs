using System.Collections.Generic;

namespace DOW
{

    public class ChapterData : TableData
    {
        private static ChapterTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>ChapterData</returns>
        public static ChapterData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<ChapterTable>();

            return table.Get(key);
        }

        public ChapterData(Dictionary<string, string> data) : base(data) { }

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
