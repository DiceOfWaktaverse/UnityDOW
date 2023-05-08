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
            string name = "start"; // Your Button Name
            GameObject startButton = GameObject.Find(name);
            Assert.NotNull(startButton);
            var setupButton = startButton.GetComponent<Button>();
            setupButton.onClick.AddListener(Clicked);
            setupButton.onClick.Invoke();
            Assert.IsTrue(clicked);
            yield return new WaitForSeconds(2);
        }

        private void Clicked()
        {
            clicked = true;
        }
    }
}