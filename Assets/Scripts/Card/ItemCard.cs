using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class ItemCard : Card
    {
        public List<Skill> skills { get; protected set; }

        public ItemCard(CardData datum) : base(datum)
        {
            ItemCardData itemCardDatum = ItemCardData.Get(datum.GetKey());
            // TODO: 스킬은 사실 카드 마다 생성할 필요 없이 한 종류의 스킬당 한번만 생성하면 됨. 이 부분 추후에 구현할 것

            for (int i = 0; i < itemCardDatum.Skils.Count; i++)
            {
                Debug.Log(itemCardDatum.Skils[i]);
            }

            skills = itemCardDatum.Skils.Select(x => new Skill(x)).ToList();
        }
    }
}