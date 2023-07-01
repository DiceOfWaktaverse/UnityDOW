using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class Skill
    {

        public string Key { get; protected set; } = "";
        public string Label { get; protected set; } = "";
        public List<eSkillType> Type { get; protected set; } = new List<eSkillType>();
        public string TriggerDescription { get; protected set; } = "";
        public string SummonDescription { get; protected set; } = "";
        public string Trigger { get; protected set; } = "";
        public string Summon { get; protected set; } = "";

        public Skill(string key) {
            SkillData datum = SkillData.Get(key);
            Key = key;
            Label = datum.Label;
            Type = datum.Type;
            TriggerDescription = datum.TriggerDescription;
            SummonDescription = datum.SummonDescription;
            Trigger = datum.Trigger;
            Summon = datum.Summon;
        }

        public string DescriptionText(Card card) {

            if (card is CharacterCard characterCard) {

                string replacedSummonDescription = SummonDescription
                    .Replace("SKILL_FACTOR", characterCard.SkillFactor.ToString())
                    .Replace("EFFECT_FACTOR", characterCard.EffectFactor.ToString())
                    .Replace("RECOVERY_FACTOR", characterCard.RecoveryFactor.ToString())
                    .Replace("DEFENCE_FACTOR", characterCard.DefenceFactor.ToString());

                if (Type.Contains(eSkillType.DICE)) {
                    return TriggerDescription + " " + replacedSummonDescription;
                } 
            }

            return TriggerDescription + " " + SummonDescription;
        }

    }

}