using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class BattleInfo : IInfo
    {
        // TODO: 최대 카드 갯수 정하기, 현재는 12개로 한함
        public List<Card> Hand { get; protected set; } = new List<Card>();

        // 5개 슬롯으로 한정함
        public List<LevelingCard> Character { get; protected set; } = new List<LevelingCard>();
        public List<Card> Coffin { get; protected set; } = new List<Card>();

        public FieldCard FriendlyField { get; protected set; } = null;
        public FieldCard EnemyField { get; protected set; } = null;

        // 전투에서 활용될 시드, 전투 시작시 생성되며 전투가 끝날때까지 유지된다.
        public long BattleSeed { get; private set; } = -1;

        public long DiceSeed {get; private set;} = -1;

        public int DiceCount { get; set; } = 0;
        public int Turn { get; private set; } = 0;

        public BattleInfo()
        {
            Initialize();
        }

        public void Initialize()
        {
            BattleSeed = (int)System.DateTime.Now.Ticks;
            DiceSeed = (int)System.DateTime.Now.Ticks;
        }

        public void SaveInfo()
        {
        }

        public void LoadInfo()
        {
        }
    }
}