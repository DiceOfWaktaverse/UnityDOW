using System;
using System.Collections.Generic;

namespace DOW
{
    public class TagData : TableData
    {
        private static TagTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>TagData</returns>
        public static TagData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<TagTable>();

            return table.Get(key);
        }

        public TagData(Dictionary<string, string> data) : base(data) { }

        public string Slug { get; protected set; } = "";
        public string Label { get; protected set; } = "";
        public string Description { get; protected set; } = "";

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
                    case "SLUG":
                        Slug = it.Current.Value;
                        break;
                    case "LABEL":
                        Label = it.Current.Value;
                        break;
                    case "DESCRIPTION":
                        Description = it.Current.Value;
                        break;
                }
            }
        }
    }
}
