using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    const string AnimationName_cidle = "cidle";
    const string AnimationName_basic = "basic";

    const string AnimationName_die = "die";
    const string AnimationName_die1 = "die1";
    const string AnimationName_die2 = "die2";

    const string AnimationName_down = "down";
    const string AnimationName_up = "up";

    const string AnimationName_run = "run";

    const string AnimationName_upwait = "upwait";

    const string AnimationName_Attack1Full = "Attack_full/Attack1_full";
    const string AnimationName_Attack2Full = "Attack_full/Attack2_full";
    const string AnimationName_Attack3Full = "Attack_full/Attack3_full";
    const string AnimationName_Attack4Full = "Attack_full/Attack4_full";
    const string AnimationName_DamageFull = "Attack_full/Damage_full";

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setAnimation_Idle() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_cidle, true);

    public void setAnimation_Basic() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_basic, true);
    public void setAnimation_Up() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_up, true);
    public void setAnimation_Down() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_down, true);
    public void setAnimation_Run() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_run, true);
    public void setAnimation_Upwait() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_upwait, true);

    public void setAnimation_Die() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_die, false);
    public void setAnimation_Die1() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_die1, false);
    public void setAnimation_Die2() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_die2, false);


    public void setAnimation_Attack1Full() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_Attack1Full, false);
    public void setAnimation_Attack2Full() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_Attack2Full, false);
    public void setAnimation_Attack3Full() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_Attack3Full, false);
    public void setAnimation_Attack4Full() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_Attack4Full, false);
    public void setAnimation_DamageFull() =>
        skeletonAnimation.AnimationState.SetAnimation(0, AnimationName_DamageFull, false);

}
