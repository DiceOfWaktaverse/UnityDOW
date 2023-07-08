using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DOW
{
    public class LevelingCard : Card
    {
        public string Card { get; protected set; } = "";
        public string Level { get; protected set; } = "1";
        public eLevelingType LevelingType { get; protected set; } = eLevelingType.NONE;
        public string LevelingDescription { get; protected set; } = "";

        public float Hp { get; protected set; } = 0f;
        public float Damage { get; protected set; } = 0f;
        public float Recovery { get; protected set; } = 0f;
        public float Defence { get; protected set; } = 0f;
        public float SkillFactor { get; protected set; } = 0f;
        public float EffectFactor { get; protected set; } = 0f;
        public float RecoveryFactor { get; protected set; } = 0f;
        public float DefenceFactor { get; protected set; } = 0f;

        private CardData baseDatum;

        public LevelingCard(CardData datum) : base(datum)
        {
            baseDatum = datum;
            SetLevel(1);
        }

        public virtual void SetLevel(int level)
        {
            LevelingData characterCardDatum = LevelingData.Get(baseDatum.GetKey() + level.ToString());
            if (characterCardDatum == null)
                return;

            Card = characterCardDatum.Card;
            Level = characterCardDatum.Level;
            LevelingType = characterCardDatum.LevelingType;
            LevelingDescription = characterCardDatum.LevelingDescription;
    
            Hp = characterCardDatum.Hp;
            Damage = characterCardDatum.Damage;
            Recovery = characterCardDatum.Recovery;
            Defence = characterCardDatum.Defence;
            SkillFactor = characterCardDatum.SkillFactor;
            EffectFactor = characterCardDatum.EffectFactor;
            RecoveryFactor = characterCardDatum.RecoveryFactor;
            DefenceFactor = characterCardDatum.DefenceFactor;
            Skills = characterCardDatum.Skills.Select(x => new Skill(x)).ToList();
        }
    }
}
