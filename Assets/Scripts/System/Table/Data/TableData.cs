using System;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class TableData : ITableData
    {
        protected string UNIQUE_KEY { get; private set; }

        protected Dictionary<string, string> data = null;
        protected static Dictionary<Type, List<string>> data_keys = new Dictionary<Type, List<string>>();

        public TableData(List<string> data)
        {
            SetData(data);
        }

        public virtual void Init() { }
        public string GetKey() { return UNIQUE_KEY; }
        public static void SetDataKeys(Type type, List<string> key)
        {
            data_keys[type] = key;
        }

        public virtual void SetData(List<string> datas)
        {
            if (data_keys == null || data_keys[GetType()] == null)
            {
#if UNITY_EDITOR
                Debug.LogError("not found data keys");
#endif
                return;
            }

            var key = data_keys[GetType()];

            data = new Dictionary<string, string>();

            for (int i = 0; i < key.Count; i++)
            {
                data[key[i]] = datas[i];
            }

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
    }
}
