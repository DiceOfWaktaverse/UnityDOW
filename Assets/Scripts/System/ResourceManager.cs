using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
	public enum eResourcePath
    {
		NONE = 0,
		START = 1,

		POPUP = START,
		DATA,
		SOUND,
		ILLUST,
		PREFABS,

		MAX
    }

	public class ResourceManager : IManagerBase
    {
        public static ResourceManager instance = null;
        public static ResourceManager Instance
        {
            get
            {
                if(instance == null)
					instance = new ResourceManager();

				return instance;
            }
        }

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void InitPlayMode()
		{
			instance = null;
		}

		public void Initialize() { }
		
		public static T GetResource<T>(eResourcePath path, string fileName) where T : Object
		{
			return GetResource<T>(path.GetPath(fileName));
		}
		private static T GetResource<T>(string path) where T : Object //어지간하면 사용하지 맙시다.
		{
			return Resources.Load<T>(path);
		}
		public static T[] GetResources<T>(string path) where T : Object
		{
			return Resources.LoadAll<T>(path);
		}
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path);
		}

		public static IEnumerator LoadAsyncPaths(List<string> targets)
		{
			if (targets == null)
				yield break;

			var reqDic = new Dictionary<ResourceRequest, bool>();

			var targetCount = targets.Count;
			for (int i = 0; i < targetCount; ++i)
            {
				reqDic.Add(Resources.LoadAsync(targets[i]), false);
			}

			var wait = new WaitForSeconds(0.1f);

			var keys = new List<ResourceRequest>(reqDic.Keys);
			var keysCount = keys.Count;
			var pathCount = 0;
			while(pathCount < targetCount)
			{
				yield return wait;

				for(int i = 0; i < keysCount; ++i)
                {
					var key = keys[i];
					if (key == null)
						continue;

					if (!reqDic[key] && key.isDone)
					{
						reqDic[key] = true;
						pathCount++;
					}
				}
			}

			yield break;
        }

		public void Update(float dt) {}
    }

}
