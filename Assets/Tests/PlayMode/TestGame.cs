using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace TestGame
{
    public class PlayModeTestGame
    {
        bool clicked = false;

        [SetUp]
        public void SetUp()
        {
            // Modify this with your first scene in the game           
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }

        [UnityTest]
        public IEnumerator TestMenu()
        {
            var gameObject = new GameObject();
            GameObject UI = GameObject.Find("UI");
            Assert.NotNull(UI);
            yield return new WaitForSeconds(2);
        }

        private void Clicked()
        {
            clicked = true;
        }
    }
}