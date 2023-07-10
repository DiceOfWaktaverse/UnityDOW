using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class BattleInfo : GameInfo
    {
        public FieldCard EnemyField { get; protected set; } = null;

        public int Turn { get; private set; } = 0;

        public BattleInfo()
        {
            Initialize();
        }

        // 배틀이 시작될 때 호출
        public void ForkGameInfo()
        {
            GameInfo gameInfo = UserInfo.Instance.GetInfo<GameInfo>();

            Hand = gameInfo.Hand;
            Character = gameInfo.Character;
            Coffin = gameInfo.Coffin;
            FriendlyField = gameInfo.FriendlyField;
            ShopSeed = gameInfo.ShopSeed;
            BatlteSeed = gameInfo.BatlteSeed;
        }
        
        // 배틀이 끝날 때 호출
        public void MergeGameInfoAndClear() 
        {
            GameInfo gameInfo = UserInfo.Instance.GetInfo<GameInfo>();

            gameInfo.Hand = Hand;
            gameInfo.Character = Character;
            gameInfo.Coffin = Coffin;
            gameInfo.FriendlyField = FriendlyField;
            gameInfo.ShopSeed = ShopSeed;
            gameInfo.BatlteSeed = BatlteSeed;

            // clear
            Hand = null;
            Character = null;
            Coffin = null;
            FriendlyField = null;
            ShopSeed = -1;
            BatlteSeed = -1;
        }
    }
}