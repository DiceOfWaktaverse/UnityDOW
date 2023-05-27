using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class MainMenuState : StateBase
    {
        //private MainMenuPopup mainMenuPopup = null;
    
        public override bool OnEnter()
        {
            //mainMenuPopup = MainMenuPopup.OpenPopup();

            return base.OnEnter();
        }

        public override bool OnExit()
        {
            //if (mainMenuPopup != null)
            //{
            //    mainMenuPopup.closeMainMenu();
            //}
            return base.OnExit();
        }
    }
}