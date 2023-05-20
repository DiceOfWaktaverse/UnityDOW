using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW {
public class StartSplashState : StateBase
{
    public override bool OnEnter() 
    {
        Debug.Log("StartSplashState OnEnter");
        return base.OnEnter();
    }

    public override bool OnExit()
    {
        Debug.Log("StartSplashState OnExit");
        return base.OnExit();
    }
}
}