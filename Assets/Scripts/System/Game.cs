using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DOW
{
    public class Game : PersistentSingleton<Game>
    {

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start()
        {
            Application.targetFrameRate = 60;

            DOWGameManager.Instance.Init();
            UIManager.Instance.InitUI(eSceneType.NONE);

            SceneManager.LoadScene("StartMenu");
		}

        // Update is called once per frame
        void Update()
		{
			DOWGameManager.Instance.Update(Time.deltaTime);
        }
	}
}
