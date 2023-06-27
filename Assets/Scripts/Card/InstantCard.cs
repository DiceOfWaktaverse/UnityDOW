using System.Linq;
using System.Collections.Generic;

namespace DOW
{
    public class InstantCard : Card
    {
        public List<Skill> skills { get; protected set; }

        public InstantCard(CardData datum) : base(datum) {
            InstantCardData instantCardDatum = InstantCardData.Get(datum.GetKey());
            // TODO: 스킬은 사실 카드 마다 생성할 필요 없이 한 종류의 스킬당 한번만 생성하면 됨. 이 부분 추후에 구현할 것
            skills = instantCardDatum.Skils.Select(x => new Skill(x)).ToList();
        }
    }

}