using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eInfoType
    {
        NONE = -1,
        START,

        CARD = START,
        BATTLE,
        GOODS,
        SHOP,
        HISTORY,

        MAX
    }
    public interface IInfo
    {
        /// <summary>
        /// 초기화
        /// </summary>
        void Initialize();
        /// <summary>
        /// Save 직렬화 및 저장.
        /// </summary>
        void SaveInfo();
        /// <summary>
        /// Load 블러오기.
        /// </summary>
        void LoadInfo();
    }
}