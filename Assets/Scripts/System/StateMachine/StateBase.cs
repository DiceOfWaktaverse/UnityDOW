using System;

namespace DOW
{
    public interface IStateBase
    {
        bool OnEnter();
        bool OnExit();
        bool OnPause();
        bool OnResume();
        bool Update(float dt);
    }
    public abstract class StateBase : IStateBase
    {
        protected bool isPause = false;
        public bool IsEnter { get; protected set; } = false;
        public bool IsPlaying { get { return IsEnter && !isPause; } }
        public virtual bool OnEnter()
        {
            if (IsEnter == true) return false;
            IsEnter = true;
            return true;
        }
        public virtual bool OnExit()
        {
            if (IsEnter == false) return false;
            IsEnter = false;
            return true;
        }
        public virtual bool OnPause()
        {
            if (isPause == true) return false;
            isPause = true;
            return true;
        }
        public virtual bool OnResume()
        {
            if (isPause == false) return false;
            isPause = false;
            return true;
        }
        public virtual bool Update(float dt)
        {
            return IsPlaying;
        }
    }
}