using UnityEditor;
using UnityEngine;

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
            UIManager.Instance.InitUI(eSceneType.None);
		}

        // Update is called once per frame
        void Update()
		{
			DOWGameManager.Instance.Update(Time.deltaTime);

			if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }
	}
}
