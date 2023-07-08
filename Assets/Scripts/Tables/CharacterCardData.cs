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

        public string Card { get; protected set; } = "";
        
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
                }
            }
        }
    }
}
