namespace DOW
{
    public class TimeEnable : TimeObject
    {
        protected override void Start() { }
        protected override void OnDestroy() { }

        protected virtual void OnEnable()
        {
            Init();
        }

        protected virtual void OnDisable()
        {
            Clear();
        }
    }
}
