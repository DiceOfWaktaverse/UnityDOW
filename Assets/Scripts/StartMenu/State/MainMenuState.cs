using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class MainMenuState : StateBase
    {
        private MainMenuPopup mainMenuPopup = null;
    
        public override bool OnEnter()
        {
            Debug.Log("MainMenuState OnEnter");
            mainMenuPopup = MainMenuPopup.OpenPopup();

            return base.OnEnter();
        }

        public override bool OnExit()
        {
            Debug.Log("MainMenuState OnExit");
            if (mainMenuPopup != null)
            {
                mainMenuPopup.closeMainMenu();
            }
            return base.OnExit();
        }
    }
}