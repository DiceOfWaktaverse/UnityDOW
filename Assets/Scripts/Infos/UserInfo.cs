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
        public static UserInfo Instance { get { return DOWGameManager.Instance.User; } }
        public eDifficulty Difficulty { get; set; } = eDifficulty.NONE;
        public string Progress { get; private set; } = "";
        public bool IsNewGame { get; set; } = true;

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
        public T GetInfo<T>() where T : class, IInfo
        {
            var type = typeof(T);
            if (type == typeof(CardInfo))
            {
                return Infos[eInfoType.CARD] as T;
            }
            else if (type == typeof(BattleInfo))
            {
                return Infos[eInfoType.BATTLE] as T;
            }
            else if (type == typeof(GoodsInfo))
            {
                return Infos[eInfoType.GOODS] as T;
            }
            else if (type == typeof(ShopInfo))
            {
                return Infos[eInfoType.SHOP] as T;
            }
            return null;
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