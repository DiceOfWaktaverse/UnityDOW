using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eInfoType
    {
        NONE = -1,
        START,


        PERSISTENT = START,
        GAME,
        BATTLE,


        CARD,


        MAX
    }
    public interface IInfo
    {
        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        void Initialize();
        /// <summary>
        /// Save ����ȭ �� ����.
        /// </summary>
        void SaveInfo();
        /// <summary>
        /// Load ��������.
        /// </summary>
        void LoadInfo();
    }
}