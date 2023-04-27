namespace DOW
{
    public abstract class Singleton<T> : ISingleton where T : class, ISingleton, new()
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    instance.Initialize();
                }
                return instance;
            }
        }
        public abstract void Initialize();
    }
}