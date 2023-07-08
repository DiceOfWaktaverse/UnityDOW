using UnityEngine;

namespace DOW
{
	public class PersistentSingleton<T> : MonoBehaviour where T : Component
	{
		protected bool _enabled;
		public static T Instance { get; private set; } = null;

		protected virtual void Awake()
		{
			if (!Application.isPlaying)
				return;

			if (Instance == null)
			{
				Instance = this as T;
				DontDestroyOnLoad(transform.gameObject);
				_enabled = true;
			}
			else if (this != Instance)
				Destroy(gameObject);
		}
	}
}
