using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eStatCategory
    {
        START = 0,

        BASE = START,       //Base 카드 능력치
        ADD,                //+ 증가 수치
        RATIO,              //% 증가 수치
        TOTAL,              //최종 계산된 스텟 카테고리

        MAX
    }
    public enum eStatType
    {
        NONE = 0,
        START = 1,

        HP = START,
        ATK,
        DEF,

        MAX
    }
    public abstract class UnitStat
    {
        //퍼센트 능력치 증가를 어떤 비율로 보관하고 컨버팅 할지 설정.
        protected readonly float CONVERT_FLOAT = 0.01f;
        protected Dictionary<eStatType, Dictionary<eStatCategory, float>> Stat { get; set; } = null;
        protected Dictionary<eStatType, bool> StatDirty { get; set; } = null;

        #region 초기화
        public bool Initialze()
        {
            if (Stat == null)
                Stat = new();
            else
                Stat.Clear();

            if (StatDirty == null)
                StatDirty = new();
            else
                StatDirty.Clear();

            InitialzeStat();
            SetStat();

            return true;
        }
        //사용 스텟 결정
        protected abstract void InitialzeStat();
        //스텟 초기화
        protected virtual void SetStat()
        {
            foreach(var current in StatDirty)
            {
                if (!Stat.ContainsKey(current.Key))
                    Stat.Add(current.Key, new());

                for (eStatCategory i = eStatCategory.START; i < eStatCategory.MAX; ++i)
                {
                    Stat[current.Key][i] = 0f;
                }
            }
        }
        #endregion
        #region 증/감
        public void IncreaseStat(eStatCategory category, eStatType type, float stat)
        {
            if (Stat == null || !Stat.ContainsKey(type))
                return;

            if (!Stat[type].ContainsKey(category))
                Stat[type][category] = 0f;

            Stat[type][category] += stat;
            SetDirty(type, true);
        }
        public void DecreaseStat(eStatCategory category, eStatType type, float stat)
        {
            if (Stat == null || !Stat.ContainsKey(type))
                return;

            if (!Stat[type].ContainsKey(category))
                Stat[type][category] = 0f;

            Stat[type][category] -= stat;
            SetDirty(type, true);
        }
        #endregion
        #region 스텟 가져오기
        protected float GetStat(eStatCategory category, eStatType type)
        {
            if (Stat == null || !Stat.ContainsKey(type))
                return 0f;

            if (!Stat[type].ContainsKey(category))
                return 0f;

            return Stat[type][category];
        }
        //외부는 아마 토탈스텟만 사용할 것으로 보임
        public virtual float GetTotalStat(eStatType type)
        {
            return GetStat(eStatCategory.TOTAL, type);
        }
        public virtual int GetTotalStatInt(eStatType type)
        {
            return Mathf.FloorToInt(GetTotalStat(type));
        }
        #endregion
        #region 갱신 및 Dirty
        //Total 갱신하는 부분
        public void CalcStat(eStatType type)
        {
            if (!GetDirty(type))
                return;

            float baseStat = GetStat(eStatCategory.BASE, type);
            float addStat = GetStat(eStatCategory.ADD, type);
            float ratioStat = 1.0f + GetStat(eStatCategory.RATIO, type) * CONVERT_FLOAT;
            float totalStat = (baseStat + addStat) * ratioStat;

            Stat[type][eStatCategory.TOTAL] = totalStat;
            SetDirty(type, false);
        }
        public void AllCalcStat()
        {
            for(eStatType i = eStatType.START; i < eStatType.MAX; ++i)
            {
                CalcStat(i);
            }
        }
        protected void SetDirty(eStatType type, bool value)
        {
            if (StatDirty == null)
                return;

            StatDirty[type] = value;
        }
        protected bool GetDirty(eStatType type)
        {
            if (StatDirty == null || !StatDirty.ContainsKey(type))
                return false;

            return StatDirty[type];
        }
        //차후 MinMax값 있을 시 타입 별 값 적용하여 사용
        protected float CheckStatMinMax(eStatType type, float value)
        {
            return value;
        }
        #endregion
    }
}
