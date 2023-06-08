using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DOW
{
    public class DifficultyPopupData //다른 데이터가 필요할 경우 사용예정.
    {
    }
    public class DifficultyPopup : Popup<DifficultyPopupData>, EventListenerBase
    {
        public override void Initialize()
        {
        }

        public override void InitializeUI()
        {
        }

        public override void Refresh()
        {
        }

        public static DifficultyPopup OpenPopup()
        {
            DifficultyPopup popup = PopupManager.OpenPopup<DifficultyPopup>("DifficultyPopup");
            return popup;
        }

        public static void SelectDifficulty()
        {
            // Get GameObject name of selected Button
            string difficulty = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

            if (difficulty == "EASY") {
                UserInfo.Instance.Difficulty = eDifficulty.EASY;
            } else if (difficulty == "HARD") {
                UserInfo.Instance.Difficulty = eDifficulty.HARD;
            } else if (difficulty == "CHAOS") {
                UserInfo.Instance.Difficulty = eDifficulty.CHAOS;
            } else if (difficulty == "GLITCH") {
                UserInfo.Instance.Difficulty = eDifficulty.GLITCH;
            } else if (difficulty == "CRASH") {
                UserInfo.Instance.Difficulty = eDifficulty.CRASH;
            }

            EventManager.TriggerEvent(StartMenuEventType.DifficultySelected);
        }

        public virtual void closePreference()
        {
            ClosePopup();
        }
    }
}