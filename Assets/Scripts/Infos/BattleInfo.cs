using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class BattleInfo : IInfo
    {
        public BattleInfo()
        {
            Initialize();
        }
        public long BatlteSeed { get; private set; } = -1;

        public void Initialize()
        {
        }
        public void SaveInfo()
        {
        }
        public void LoadInfo()
        {
        }
    }
}