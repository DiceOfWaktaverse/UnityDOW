using DOW;
using System;
using System.Collections.Generic;

namespace DOW
{
    public class TableManager : IManagerBase
    {
        public TableManager()
        {
            tables = new Dictionary<Type, ITableBase>();
        }
        protected static TableManager instance = null;
        public static TableManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new TableManager();

                    #region 테이블 세팅
                    instance.AddTable(typeof(ChapterTable), new ChapterTable());
                    instance.AddTable(typeof(StageTable), new StageTable());
                    instance.AddTable(typeof(TagTable), new TagTable());
                    instance.AddTable(typeof(CardPackTable), new CardPackTable());
                    instance.AddTable(typeof(CardTable), new CardTable());
                    instance.AddTable(typeof(LevelingTable), new LevelingTable());
                    instance.AddTable(typeof(SkillTable), new SkillTable());
                    instance.AddTable(typeof(CharacterCardTable), new CharacterCardTable());
                    instance.AddTable(typeof(EnemyTable), new EnemyTable());
                    instance.AddTable(typeof(FieldCardTable), new FieldCardTable());
                    instance.AddTable(typeof(InstantCardTable), new InstantCardTable());
                    instance.AddTable(typeof(ItemCardTable), new ItemCardTable());
                    #endregion
                }
                return instance;
            }
        }
        private Dictionary<Type, ITableBase> tables = null;
        public void Initialize()
        {
            if(tables == null)
                return;

            var it = tables.GetEnumerator();
            while(it.MoveNext())
            {
                it.Current.Value.Init();
            }
        }
        public void Clear()
        {
            if (tables == null)
                return;

            var it = tables.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.DataClear();
            }
        }

        public bool AddTable(Type type, ITableBase target)
        {
            if (tables == null)
                return false;

            if (tables.ContainsKey(type)) //매니저 중복
                return false;

            tables.Add(type, target);
            return true;
        }

        public static T GetTable<T>() where T : class, ITableBase
        {
            if(Instance.tables == null)
                return null;

            var type = typeof(T);
            if(Instance.tables.ContainsKey(type) && Instance.tables[type] is T)
                return Instance.tables[type] as T;

            return null;
        }

        public void Update(float dt) {}
    }
}
