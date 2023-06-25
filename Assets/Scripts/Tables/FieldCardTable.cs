using System.Collections.Generic;

namespace DOW
{
    public class FieldCardTable : TableBase<FieldCardData>
    {
        public override void SetTable(List<Dictionary<string, string>> array)
        {
            DataClear();

            for(int i = 0, count = array.Count; i < count; ++i)
            {
                Add(new(array[i]));
            }
        }
    }
}