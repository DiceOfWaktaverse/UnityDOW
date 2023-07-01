
using System.Collections.Generic;

namespace DOW
{
    public class EnemyCard : LevelingCard 
    {
        public List<string> Pattern { get; protected set; } = new List<string>();

        private CardData baseDatum;

        public EnemyCard(CardData datum) : base(datum) {
            baseDatum = datum;
            SetLevel(1);
        }

        public override void SetLevel(int i) 
        {
            base.SetLevel(i);
            EnemyData enemyData = TableManager.GetTable<EnemyTable>().Get(baseDatum.GetKey() + i.ToString());
            Pattern = enemyData.Pattern;
        }
    }
}