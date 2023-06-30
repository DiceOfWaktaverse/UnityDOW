using System.Collections.Generic;
using System.Linq;

namespace DOW
{
    public abstract class TableBase<T> : ITableBase where T : class, ITableData
    {
        protected Dictionary<string, T> datas = null;
        
        public virtual void Init()
        {
            if (datas == null)
                datas = new Dictionary<string, T>();
            else
                datas.Clear();
        }
        public abstract void SetTable(List<Dictionary<string, string>> array);
        public virtual void DataClear()
        {
            if (datas == null)
                return;

            var it = datas.GetEnumerator();

            while (it.MoveNext())
            {
                it.Current.Value.Init();
            }

            datas.Clear();
        }

        public virtual T Get(object key)
        {
            return Get(key.ToString());
        }
        public virtual T Get(int key)
        {
            return Get(key.ToString());
        }
        public virtual T Get(string key)
        {
            if (ContainsKey(key))
            {
                return datas[key];
            }
            return null;
        }

        public List<T> GetAllList()
        {
            return datas.Values.ToList();
        }

        public Dictionary<string, T> GetAllDic()
        {
            return datas;
        }

        public List<string> GetKey()
        {
            return datas.Keys.ToList();
        }

        protected virtual bool Add(T data)
        {
            if (ContainsKey(data.GetKey()))
            {
                UnityEngine.Debug.LogError("TableBase Error : 중복 키 => " + data.GetKey());
                return false;
            }

            datas.Add(data.GetKey(), data);
            return true;
        }

        protected virtual bool Add(string key, T data)
        {
            if (ContainsKey(key))
            {
                UnityEngine.Debug.LogError("TableBase Error : 중복 키 => " + data.GetKey());
                return false;
            }

            datas.Add(key, data);
            return true;
        }

        protected virtual bool Remove(T data)
        {
            return Remove(data.GetKey());
        }

        protected virtual bool Remove(string _index)
        {
            if(ContainsKey(_index))
            {
                return datas.Remove(_index);
            }
            return false;
        }
        public bool ContainsKey(object key)
        {
            return ContainsKey(key.ToString());
        }
        public virtual bool ContainsKey(string key)
        {
            return datas.ContainsKey(key);
        }

        public override string ToString()
        {
            string str = "";
            var it = datas.GetEnumerator();

            while (it.MoveNext())
            {
                str += it.Current.Value.ToString() + "\n";
            }

            return str;
        }
    }
}
