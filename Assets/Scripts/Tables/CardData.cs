using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eCardType
    {
        NONE,
        CHAR,
        INST,
        ITEM,
        FIELD,
        MAX
    }

    public enum eRestriction
    {
        NONE,
        SYSTEM,
        NO_RESURRECTION,
        MAX
    }

    public class CardData : TableData
    {
        private static CardTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>CardData</returns>
        public static CardData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<CardTable>();

            return table.Get(key);
        }

        public CardData(Dictionary<string, string> data) : base(data) { }

        public string CardPack { get; protected set; } = "";
        public eCardType Type { get; protected set; } = eCardType.NONE;
        public string Illust { get; protected set; } = "9999";
        public string Label { get; protected set; } = "";
        public string Description { get; protected set; } = "";
        public List<string> Tags { get; protected set; } = new List<string>();
        public List<eRestriction> Restrictions { get; protected set; } = new List<eRestriction>();

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
                        break;
                    case "CARD_PACK_KEY":
                        CardPack = it.Current.Value;
                        break;
                    case "TYPE":
                        eCardType type;
                        if (Enum.TryParse<eCardType>(it.Current.Value, out type))
                            Type = type;
                        else
                            Type = eCardType.NONE;
                        break;
                    case "ILLUST":
                        if (it.Current.Value == "") break;
                        Illust = it.Current.Value;
                        break;
                    case "LABEL":
                        Label = it.Current.Value;
                        break;
                    case "DESCRIPTION":
                        Description = it.Current.Value;
                        break;
                    case "TAGS":
                        if (it.Current.Value != "")
                            Tags = it.Current.Value.Split('|').ToList<string>();
                        break;
                    case "RESTRICTION":
                        string[] fragment = it.Current.Value.Split('|');
                        for (int i = 0; i < fragment.Length; i++)
                        {
                            eRestriction restriction;
                            if (Enum.TryParse<eRestriction>(fragment[i], out restriction))
                                Restrictions.Add(restriction);
                        }
                        break;
                }
            }
        }
    }
}
