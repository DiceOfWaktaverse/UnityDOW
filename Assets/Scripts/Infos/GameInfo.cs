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

    public enum eSlotType
    {
        NONE = -1,
        START,
        SLOT1 = START,
        SLOT2,
        SLOT3,
        SLOT4,
        SLOT5,
        MAX
    }

    public class GameInfo : PersistentInfo
    {
        // Initial Data
        public bool IsNewGame { get; set; } = true;

        // Game Domain Data
        public eDifficulty Difficulty { get; set; } = eDifficulty.NONE;
        public string Progress { get; set; } = "";

        // Card Data
        public List<Card> Hand { get; set; } = new List<Card>(); // TODO: 최대 카드 갯수 정하기, 현재는 12개로 한함
        public List<Card> Coffin { get; set; } = new List<Card>();
        public Dictionary<eSlotType, CharacterCard> Character { get; set; } = new Dictionary<eSlotType, CharacterCard>();

        // Field Data
        public FieldCard FriendlyField { get; set; } = null;

        // Seed Data
        public long ShopSeed { get; set; } = -1;
        public int ShopIndex { get; set; } = -1;
        public long BatlteSeed { get; set; } = -1;
        public int BattleIndex { get; set; } = -1;


        public GameInfo()
        {
            Initialize();
        }

        public void LoadGameInfo() {

        }

        public void InitializeGameInfo() {
            Progress = StageData.First().GetKey();
            ShopSeed = System.DateTime.Now.Ticks;
            BatlteSeed = System.DateTime.Now.Ticks + 1000; // setting some offset
            ShopIndex = 0;
            BattleIndex = 0;
        }

        public void AddCharacter(eSlotType slotType, CharacterCard characterCard)
        {
            if (Character.ContainsKey(slotType))
            {
                Character[slotType] = characterCard;
            }
            else
            {
                Character.Add(slotType, characterCard);
            }
        }

        public void AddHand(Card card)
        {
            Hand.Add(card);
        }

        public override string ToString() {
            string str = "";
            str += "Hand: ";
            foreach (Card card in Hand) {
                str += card.ToString() + ", ";
            }
            str += "\n";

            str += "Character: ";
            foreach (KeyValuePair<eSlotType, CharacterCard> pair in Character) {
                str += pair.Key.ToString() + ": " + pair.Value.ToString() + ", ";
            }

            str += "Coffin: ";
            foreach (Card card in Coffin) {
                str += card.ToString() + ", ";
            }
            str += "\n";

            str += "FriendlyField: ";
            if (FriendlyField != null) {
                str += FriendlyField.ToString();
            }
            str += "\n";

            // progress
            str += "Progress: " + Progress + "\n";

            str += "BatlteSeed: " + BatlteSeed + "\n";
            str += "ShopSeed: " + ShopSeed + "\n";

            return str;
        }
    }
}