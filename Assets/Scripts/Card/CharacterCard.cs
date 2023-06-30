using System.Linq;
using System.Collections.Generic;

namespace DOW {
    public class CharacterCard : Card
    {
        public string Character { get; protected set; } = "";
        public string Level { get; protected set; } = "1";
        public float Hp { get; protected set; } = 0f;
        public float Damage { get; protected set; } = 0f;
        public float Recovery { get; protected set; } = 0f;
        public float Defence { get; protected set; } = 0f;
        public float SkillFactor { get; protected set; } = 0f;
        public float RecoveryFactor { get; protected set; } = 0f;
        public float DefenceFactor { get; protected set; } = 0f;
        public string LevelingDescription { get; protected set; } = "";
        public List<Skill> Skills { get; protected set; } = new List<Skill>();

        public CharacterCard(CardData datum) : base(datum) {

            // 캐릭터 카드의 키값은 그냥 카드 키값 뒤에 레벨 붙여서 쓰면 붙여서 쓰면 됨
            CharacterCardData characterCardDatum = CharacterCardData.Get(datum.GetKey() + "1");
            Character = characterCardDatum.Character;
            Level = characterCardDatum.Level;
            Hp = characterCardDatum.Hp;
            Damage = characterCardDatum.Damage;
            Recovery = characterCardDatum.Recovery;
            Defence = characterCardDatum.Defence;
            SkillFactor = characterCardDatum.SkillFactor;
            RecoveryFactor = characterCardDatum.RecoveryFactor;
            DefenceFactor = characterCardDatum.DefenceFactor;
            LevelingDescription = characterCardDatum.LevelingDescription;
            Skills = characterCardDatum.Skills.Select(x => new Skill(x)).ToList();
        }

        public override string ToString()
        {
            string str = "";
            str += Key + "(" + Type + "): " + Label + " " + Level;
            return str;
        }
    }
}