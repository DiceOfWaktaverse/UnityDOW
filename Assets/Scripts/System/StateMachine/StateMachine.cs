using System;
using System.Collections.Generic;

namespace DOW
{
    interface IStateMachine<T> where T : class, IStateBase
    {
        bool AddState(T state);
        T GetState(Type type);
        bool StateInit();
    }
    public abstract class SimpleStateMachine<T> : IStateMachine<T> where T : class, IStateBase
    {
        public T CurState { get; protected set; }
        protected Dictionary<Type, T> states = new Dictionary<Type, T>();

        public abstract void SetState();

        public virtual bool AddState(T state)
        {
            Type stateType = state.GetType();
            if (states.ContainsKey(stateType))
            {
                return false;
            }

            states.Add(stateType, state);
            return true;
        }

        public virtual bool ChangeState<TYPE>() where TYPE : class, IStateBase
        {
            return ChangeState(typeof(TYPE));
        }

        public virtual bool ChangeState(T state)
        {
            if (state == null)
                return false;

            if (CurState == null)
            {
                if (state.OnEnter())
                {
                    CurState = state;
                    return true;
                }
            }
            else
            {
                if (CurState.OnExit() && state.OnEnter())
                {
                    CurState = state;
                    return true;
                }
            }

            return false;
        }

        public bool ChangeState(Type state)
        {
            return ChangeState(GetState(state));
        }

        public virtual T GetState(Type type)
        {
            if (states.ContainsKey(type))
                return states[type] as T;

            return null;
        }

        public virtual TYPE GetState<TYPE>() where TYPE : class, IStateBase
        {
            var type = typeof(TYPE);
            if (states.ContainsKey(type))
                return states[type] as TYPE;

            return null;
        }

        public virtual bool StateInit()
        {
            CurStateClear();
            states.Clear();
            SetState();
            return true;
        }

        public virtual void CurStateClear()
        {
            if (CurState != null)
            {
                CurState.OnExit();
                CurState = null;
            }
        }
    }
}