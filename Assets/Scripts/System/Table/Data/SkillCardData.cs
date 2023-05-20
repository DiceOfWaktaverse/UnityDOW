using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class SkillCardData : TableData
    {
        private static SkillCardTable table = null;

        /// <summary>
        /// 추적을 용이하게 하기 위함
        /// </summary>
        /// <param name="key">카드명</param>
        /// <returns>SkillCardData</returns>
        public static SkillCardData Get(string key)
        {
            if (table == null)
                table = TableManager.GetTable<SkillCardTable>();

            return table.Get(key);
        }

        public SkillCardData(Dictionary<string, string> data): base(data) { }

        public string UniqueSkill { get; protected set; } = "";
        public string UniqueSkillValue1 { get; protected set; } = "";
        public string UniqueSkillValue2 { get; protected set; } = "";
        public string UniqueSkillValue3 { get; protected set; } = "";
        public string UniqueSkillValue4 { get; protected set; } = "";
        public string UniqueSkillValue5 { get; protected set; } = "";
        public string Trigger { get; protected set; } = "";
        public string TriggerValue1 { get; protected set; } = "";
        public string TriggerValue2 { get; protected set; } = "";
        public eCardPackTag Tags { get; protected set; } = eCardPackTag.NONE;
        public string PlaybleText { get; protected set; } = "";

        protected override string GetUniqueKeyName()
        {
            return "카드명";
        }
        public override void Init()
        {
            base.Init();
        }
        public override void SetData(Dictionary<string, string> datas)
        {
            base.SetData(datas);

            var it = datas.GetEnumerator();

            while(it.MoveNext())
            {
                switch(it.Current.Key)
                {
                    case "카드명"://상위에서 UniqueKeyName으로 동작중.
                        break;
                    case "고유 스킬":
                        UniqueSkill = it.Current.Value;
                        break;
                    case "고유 스킬 수치 1 ( a )":
                        UniqueSkillValue1 = it.Current.Value;
                        break;
                    case "고유 스킬 수치 2 ( b )":
                        UniqueSkillValue2 = it.Current.Value;
                        break;
                    case "고유 스킬 수치 3 ( c )":
                        UniqueSkillValue3 = it.Current.Value;
                        break;
                    case "고유 스킬 수치 4 ( d )":
                        UniqueSkillValue4 = it.Current.Value;
                        break;
                    case "고유 스킬 수치 5 ( e )":
                        UniqueSkillValue5 = it.Current.Value;
                        break;
                    case "조건":
                        Trigger = it.Current.Value;
                        break;
                    case "조건 수치 1 ( a )":
                        TriggerValue1 = it.Current.Value;
                        break;
                    case "조건 수치 2 ( b )":
                        TriggerValue2 = it.Current.Value;
                        break;
                    case "태그":
                        Tags = ConvertTag(it.Current.Value);
                        break;
                    case "플레이버 텍스트":
                        PlaybleText = it.Current.Value;
                        break;
                }
            }
        }

        private eCardPackTag ConvertTag(string value)
        {
            var tag = eCardPackTag.NONE;

            var tags = value.Split(",");
            if (tags != null && tags.Length > 0)
            {
                for(int i = 0, count = tags.Length; i < count; ++i)
                {
                    var current = Define.ConvertCardTag(tags[i].Trim());

                    tag |= current;
                }
            }

            return tag;
        }
    }
}