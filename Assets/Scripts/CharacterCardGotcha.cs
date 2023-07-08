using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class CharacterCardGotcha : MonoBehaviour
    {

        private float debounceTime = 0.5f;
        private float elapsedTime = 0.0f;

        void Start()
        {
            elapsedTime = 0.0f;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && elapsedTime > debounceTime)
                PopupManager.ClosePopup<CharacterCardGotchaPopup>();

            elapsedTime += Time.deltaTime;
        }
    }
}