using System;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class TableData : ITableData
    {
        protected string UNIQUE_KEY { get; private set; }

        protected Dictionary<string, string> data = null;

        public TableData(Dictionary<string, string> data)
        {
            SetData(data);
        }

        public virtual void Init() { }
        public string GetKey() { return UNIQUE_KEY; }

        public virtual void SetData(Dictionary<string, string> datas)
        {
            data = datas;
            SetUniqueID();
        }

        void SetUniqueID()
        {
            if (!data.ContainsKey(GetUniqueKeyName()))
                return;

            try
            {
                if (string.IsNullOrEmpty(data[GetUniqueKeyName()]))
                    UNIQUE_KEY = "-1";
                else
                    UNIQUE_KEY = data[GetUniqueKeyName()];
            }
            catch
            {
                UNIQUE_KEY = data[GetUniqueKeyName()];
#if UNITY_EDITOR
                throw;
#endif
            }
        }

        protected virtual string GetUniqueKeyName()
        {
            return "KEY";
        }

        protected int Int(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return int.Parse(val);
        }

        protected float Float(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0.0f;

            return float.Parse(val);
        }
        protected long Long(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return long.Parse(val);
        }

        public override string ToString()
        {
            string str = "";
            var it = data.GetEnumerator();

            while (it.MoveNext())
            {
                str += it.Current.Value + ", ";
            }

            return str;
        }
    }
}
