using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class UserInfo : IInfo
    {
        public static UserInfo Instance { get { return DOWGameManager.Instance.User; } }

        [SerializeField]
        private Dictionary<eInfoType, IInfo> Infos { get; set; } = null;
        public void Initialize()
        {
            if (Infos == null)
            {
                Infos = new Dictionary<eInfoType, IInfo>();

                AddInfo(eInfoType.PERSISTENT, new PersistentInfo());
                AddInfo(eInfoType.GAME, new GameInfo());
                AddInfo(eInfoType.BATTLE, new BattleInfo());
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
            if (type == typeof(PersistentInfo))
            {
                return Infos[eInfoType.PERSISTENT] as T;
            }
            else if (type == typeof(GameInfo))
            {
                return Infos[eInfoType.GAME] as T;
            }
            else if (type == typeof(BattleInfo))
            {
                return Infos[eInfoType.BATTLE] as T;
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