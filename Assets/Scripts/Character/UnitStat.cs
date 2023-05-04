using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eStatCategory
    {
        START = 0,

        BASE = START,       //Base ī�� �ɷ�ġ
        ADD,                //+ ���� ��ġ
        RATIO,              //% ���� ��ġ
        TOTAL,              //���� ���� ���� ī�װ�

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
        //�ۼ�Ʈ �ɷ�ġ ������ � ������ �����ϰ� ������ ���� ����.
        protected readonly float CONVERT_FLOAT = 0.01f;
        protected Dictionary<eStatType, Dictionary<eStatCategory, float>> Stat { get; set; } = null;
        protected Dictionary<eStatType, bool> StatDirty { get; set; } = null;

        #region �ʱ�ȭ
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
        //��� ���� ����
        protected abstract void InitialzeStat();
        //���� �ʱ�ȭ
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
        #region ��/��
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
        #region ���� ��������
        protected float GetStat(eStatCategory category, eStatType type)
        {
            if (Stat == null || !Stat.ContainsKey(type))
                return 0f;

            if (!Stat[type].ContainsKey(category))
                return 0f;

            return Stat[type][category];
        }
        //�ܺδ� �Ƹ� ��Ż���ݸ� ����� ������ ����
        public virtual float GetTotalStat(eStatType type)
        {
            return GetStat(eStatCategory.TOTAL, type);
        }
        public virtual int GetTotalStatInt(eStatType type)
        {
            return Mathf.FloorToInt(GetTotalStat(type));
        }
        #endregion
        #region ���� �� Dirty
        //Total �����ϴ� �κ�
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
        //���� MinMax�� ���� �� Ÿ�� �� �� �����Ͽ� ���
        protected float CheckStatMinMax(eStatType type, float value)
        {
            return value;
        }
        #endregion
    }
}
