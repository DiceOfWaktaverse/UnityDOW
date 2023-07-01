using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class CardPack
    {
        public string Key { get; protected set; } = "";
        public string Label { get; protected set; } = "";

        public CardPack(string key)
        {
            Debug.Log(key);
            CardPackData datum = TableManager.GetTable<CardPackTable>().Get(key);
            Debug.Log(datum);
            Key = key;
            Label = datum.Label;
        }
    }
}
