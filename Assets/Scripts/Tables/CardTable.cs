using System.Collections.Generic;

namespace DOW
{
    public class CardTable : TableBase<CardData>
    {
        public override void SetTable(List<Dictionary<string, string>> array)
        {
            DataClear();

            // Count - 1 인 이유는 CSV마지막 라인에 빈 라인이 있기 때문
            for (int i = 0, count = array.Count - 1; i < count; ++i)
            {
                Add(new(array[i]));
            }
        }

        // 카드 타입에 따라 키값을 반환

        public List<string> GetKeys(List<eCardType> type)
        {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (type.Contains(it.Current.Type))
                {
                    keys.Add(it.Current.GetKey());
                }
            }
            return keys;
        }

        public List<string> GetKeys(List<eCardType> type, bool includeSystemCard)
        {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (type.Contains(it.Current.Type) && (includeSystemCard || !it.Current.Restrictions.Contains(eRestriction.SYSTEM)))
                {
                    keys.Add(it.Current.GetKey());
                }
            }
            return keys;
        }

        // 카드 팩에 따라 키값을 반환
        public List<string> GetKeys(List<string> cardPackKey)
        {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (cardPackKey.Contains(it.Current.CardPack))
                {
                    keys.Add(it.Current.GetKey());
                }
            }

            return keys;
        }
    }
}