using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public class ChapterData : TableData
    {
        private static ChapterTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>SkillCardData</returns>
        public static ChapterData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<ChapterTable>();

            return table.Get(key);
        }

        public ChapterData(Dictionary<string, string> data) : base(data) { }

        public string Chapter { get; protected set; } = "";
        public string Sprite { get; protected set; } = "";
        public string Label { get; protected set; } = "";

        protected override string GetUniqueKeyName()
        {
            return "Chapter";
        }

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
                    case "Chapter"://상위에서 UniqueKeyName으로 동작중.
                        break;
                    case "Sprite":
                        Sprite = it.Current.Value;
                        break;
                    case "Label":
                        Label = it.Current.Value;
                        break;
                }
            }
        }
    }
}
