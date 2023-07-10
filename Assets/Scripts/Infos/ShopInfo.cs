using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class ShopInfo : IInfo
    {
        public long ShopSeed { get; private set; } = -1;

        public ShopInfo()
        {
            Initialize();
        }

        public void Initialize()
        {
            ShopSeed = System.DateTime.Now.Ticks;
        }
        public void SaveInfo()
        {
        }
        public void LoadInfo()
        {
        }
    }
}