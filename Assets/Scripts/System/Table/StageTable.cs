using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class StageTable : TableBase<StageData>
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