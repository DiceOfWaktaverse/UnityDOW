using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    /// <summary>
    /// 스파인의 고정된 형태의 애니메이션을 정해야합니다.
    /// </summary>
	public enum eWomanAnimation
	{
		DEFAULT,

		IDLE = DEFAULT,
		ATTACK1,
		ATTACK2,
		ATTACK3,
		ATTACK4,
		HIT,
		DEATH,
		UP,
		DOWN,
		UP_WAIT,

		MAX
	}
    /// <summary>
    /// 필요한 내용은 여기서 구현 살을 잘 붙여야 할 듯 합니다.
    /// </summary>
	public class UIWomanController : UISpineController<eWomanAnimation>
    {
        public override void InitializeStart()
        {
            base.InitializeStart();

            if (Animation == default)
                Animation = eWomanAnimation.IDLE;

            InitializeDraw();
        }

        void InitializeDraw()
        {
            SetAnimation(Animation);
            if (skeletonAni != null)
                skeletonAni.AnimationState.Complete += SpineCompleteEvent;
        }
        /// <summary>
        /// 각 상태별로 애니메이션 완료 시 반복은 미사용
        /// </summary>
        /// <param name="e">Cur Track</param>
        void SpineCompleteEvent(Spine.TrackEntry e)
        {
            if (e.Loop)
                return;

            switch (GetNameToType(e.Animation.Name))
            {
                case eWomanAnimation.ATTACK1:
                case eWomanAnimation.ATTACK2:
                case eWomanAnimation.ATTACK3:
                case eWomanAnimation.ATTACK4:
                    SetAnimation(eWomanAnimation.IDLE);
                    break;
                case eWomanAnimation.HIT:
                    break;
                case eWomanAnimation.DEATH:
                    break;
                case eWomanAnimation.UP:
                    break;
                case eWomanAnimation.DOWN:
                    break;
                case eWomanAnimation.UP_WAIT:
                    break;
                case eWomanAnimation.DEFAULT:
                default:
                    break;
            }
        }

        public override void InitializeTypeFunc()
        {
            GetTypeToLoop = IsTypeToLoop;
            GetTypeToName = GetDragonAnimTypeToName;
            GetNameToType = GetDragonAnimNameToType;
        }

        /// <summary>
        /// 필요하면 그림자 넣어서 사용.
        /// </summary>
        /// <param name="show"></param>
        public override void SetShadow(bool show)
        {
        }
        private string GetDragonAnimTypeToName(eWomanAnimation eAnim)
        {
            return eAnim switch
            {
                eWomanAnimation.ATTACK1 => "Attack_full/Attack1_full",
                eWomanAnimation.ATTACK2 => "Attack_full/Attack1_full",
                eWomanAnimation.ATTACK3 => "Attack_full/Attack1_full",
                eWomanAnimation.ATTACK4 => "Attack_full/Attack1_full",
                eWomanAnimation.HIT => "Attack_full/Damage_full",
                eWomanAnimation.DEATH => "die",
                eWomanAnimation.UP => "up",
                eWomanAnimation.DOWN => "down",
                eWomanAnimation.UP_WAIT => "upwait",
                eWomanAnimation.IDLE => "cidle",
                _ => "cidle"
            };
        }
        private eWomanAnimation GetDragonAnimNameToType(string strAnim)
        {
            return strAnim switch
            {
                "Attack_full/Attack1_full" => eWomanAnimation.ATTACK1,
                "Attack_full/Attack2_full" => eWomanAnimation.ATTACK2,
                "Attack_full/Attack3_full" => eWomanAnimation.ATTACK3,
                "Attack_full/Attack4_full" => eWomanAnimation.ATTACK4,
                "die" => eWomanAnimation.DEATH,
                "Attack_full/Damage_full" => eWomanAnimation.HIT,
                "up" => eWomanAnimation.UP,
                "down" => eWomanAnimation.DOWN,
                "upwait" => eWomanAnimation.UP_WAIT,
                "cidle" => eWomanAnimation.IDLE,
                _ => eWomanAnimation.IDLE
            };
        }
        private bool IsTypeToLoop(eWomanAnimation anim)
        {
            return anim switch
            {
                eWomanAnimation.IDLE => true,
                _ => false
            };
        }
    }
}
