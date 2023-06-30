using System.Collections.Generic;

namespace DOW
{
    public class InstantCardTable : TableBase<InstantCardData>
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
    }
}