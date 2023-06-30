using System.Collections.Generic;

namespace DOW
{
    public class CardTable : TableBase<CardData>
    {
        public override void SetTable(List<Dictionary<string, string>> array)
        {
            DataClear();

            // Count - 1 인 이유는 CSV마지막 라인에 빈 라인이 있기 때문
            for(int i = 0, count = array.Count - 1; i < count; ++i)
            {
                Add(new(array[i]));
            }
        }

        // 카드 타입에 따라 키값을 반환
        public List<string> GetKey(eCardType type)
        {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (it.Current.Type == type)
                {
                    keys.Add(it.Current.GetKey());
                }
            }
            return keys;
        }

        // 카드 팩에 따라 키값을 반환
        public List<string> GetKey(string cardPackKey) {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (it.Current.CardPack == cardPackKey)
                {
                    keys.Add(it.Current.GetKey());
                }
            }
            return keys;
        }

        // 카드 타입과 팩에 따라 키값을 반환
        public List<string> GetKey(eCardType type, string cardPackKey) {
            List<string> keys = new List<string>();
            IEnumerator<CardData> it = datas.Values.GetEnumerator();

            while (it.MoveNext())
            {
                if (it.Current.Type == type && it.Current.CardPack == cardPackKey)
                {
                    keys.Add(it.Current.GetKey());
                }
            }
            return keys;
        }
    }
}