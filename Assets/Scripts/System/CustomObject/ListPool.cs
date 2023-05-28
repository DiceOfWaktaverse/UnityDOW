using System.Collections.Generic;

namespace DOW
{
    public class ListPool<T> where T : class
    {
        public delegate void ListPoolReuse(T item);
        public delegate void ListPoolUnuse(T item);

        protected ListPoolReuse reuse = null;
        protected ListPoolUnuse unuse = null;
        protected List<T> datas = null;

        public ListPool()
        {
            reuse = null;
            unuse = null;
        }
        public ListPool(ListPoolReuse reuse, ListPoolUnuse unuse)
        {
            this.reuse = reuse;
            this.unuse = unuse;
        }
        public int Count
        {
            get
            {
                if (datas == null)
                    return 0;
                return datas.Count;
            }
        }

        public void Put(T item)
        {
            if (datas == null)
                datas = new List<T>();

            if (!datas.Contains(item))
            {
                unuse?.Invoke(item);
                datas.Add(item);
            }
        }
        public void Clear()
        {
            if (datas == null)
                datas = new List<T>();
            else
                datas.Clear();
        }
        public T Get()
        {
            var last = Count - 1;
            if (last < 0)
                return null;

            var item = datas[last];
            datas.RemoveAt(last);

            reuse?.Invoke(item);

            return item;
        }
    }
}