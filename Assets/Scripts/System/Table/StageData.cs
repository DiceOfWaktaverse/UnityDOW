using DOW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eStageType
    {
        NONE,
        NORMAL,
        ELITE,
        BOSS,
        EVENT,
        MAX
    }

    public class StageData : TableData
    {
        private static StageTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>SkillCardData</returns>
        public static StageData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<StageTable>();

            return table.Get(key);
        }

        public StageData(Dictionary<string, string> data) : base(data) { }

        public string Stage { get; protected set; } = "";
        public string Sprite { get; protected set; } = "";
        public string Label { get; protected set; } = "";
        public eStageType Type { get; protected set; } = eStageType.NONE;
        public string CompensationA { get; protected set; } = "";
        public string CompensationB { get; protected set; } = "";
        public string CompensationC { get; protected set; } = "";
        public string EnemyFieldCard { get; protected set; } = "";
        public List<string> Enemy { get; protected set; } = new List<string>();

        protected override string GetUniqueKeyName()
        {
            return "Stage";
        }

        public override void Init()
        {
            base.Init();
        }

        public override void SetData(Dictionary<string, string> datas)
        {
            base.SetData(datas);

            var it = datas.GetEnumerator();

            while (it.MoveNext())
            {
                switch (it.Current.Key)
                {
                    case "Stage"://상위에서 UniqueKeyName으로 동작중.
                        break;
                    case "Sprite":
                        Sprite = it.Current.Value;
                        break;
                    case "Label":
                        Label = it.Current.Value;
                        break;
                    case "Type":
                        eStageType type;
                        if (Enum.TryParse<eStageType>(it.Current.Value, out type))
                            Type = type;
                        else
                            Debug.LogWarning("Failed to parse eStageType : " + it.Current.Value + ")");
                            Type = eStageType.NONE;
                        break;
                    case "CompensationA":
                        CompensationA = it.Current.Value;
                        break;
                    case "CompensationB":
                        CompensationB = it.Current.Value;
                        break;
                    case "CompensationC":
                        CompensationC = it.Current.Value;
                        break;
                    case "EnemyFieldCard":
                        EnemyFieldCard = it.Current.Value;
                        break;
                    case "Enemy1":
                    case "Enemy2":
                    case "Enemy3":
                    case "Enemy4":
                    case "Enemy5":
                    case "Enemy6":
                    case "Enemy7":
                    case "Enemy8":
                    case "Enemy9":
                    case "Enemy10":
                        Enemy.Add(it.Current.Value);
                        break;
                }
            }
        }
    }
}
