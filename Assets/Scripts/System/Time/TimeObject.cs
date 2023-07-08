using UnityEngine;

namespace DOW
{
    public class TimeObject : MonoBehaviour, ITimeObject
    {
        public float Time { get; set; } = -1;
        protected VoidDelegate refresh = null;
        public VoidDelegate Refresh
        {
            get
            {
                return refresh;
            }

            set
            {
                refresh = value;
                refresh?.Invoke();
            }
        }

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void OnDestroy()
        {
            Clear();
        }

        public virtual void Init()
        {
            TimeManager.AddObject(this);
        }

        public virtual void Clear()
        {
            TimeManager.DelObject(this);
        }
    }
}
