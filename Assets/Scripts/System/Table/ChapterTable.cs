using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class ChapterTable : TableBase<ChapterData>
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