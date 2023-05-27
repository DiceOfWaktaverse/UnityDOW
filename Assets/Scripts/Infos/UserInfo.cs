using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eDifficulty
    {
        NONE = -1,
        START,

        EASY = START,
        HARD,
        CHAOS,
        GLITCH,
        CRASH,

        MAX
    }
    [System.Serializable]
    public class UserInfo : IInfo
    {
        //public static UserInfo Instance { get { return DOWGameManager.Instance.User; } }
        public eDifficulty Difficulty { get; private set; } = eDifficulty.NONE;
        public int Progress { get; private set; } = -1;
        [SerializeField]
        private Dictionary<eInfoType, IInfo> Infos { get; set; } = null;
        public void Initialize()
        {
            if (Infos == null)
            {
                Infos = new Dictionary<eInfoType, IInfo>();
                AddInfo(eInfoType.CARD, new CardInfo());
                AddInfo(eInfoType.BATTLE, new BattleInfo());
                AddInfo(eInfoType.GOODS, new GoodsInfo());
                AddInfo(eInfoType.SHOP, new ShopInfo());
            }
            else
            {
                var it = Infos.GetEnumerator();
                while(it.MoveNext())
                {
                    it.Current.Value.Initialize();
                }
            }
        }
        /// <summary>
        /// ���� ���̺굵 �ؾ���
        /// </summary>
        public void SaveInfo()
        {
            var it = Infos.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.SaveInfo();
            }
        }
        /// <summary>
        /// ���� �ε嵵 �ؾ���
        /// </summary>
        public void LoadInfo()
        {
            var it = Infos.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.LoadInfo();
            }
        }
        private void AddInfo(eInfoType type, IInfo info)
        {
            Infos.Add(type, info);
        }
    }
}