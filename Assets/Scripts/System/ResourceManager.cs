using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class ResourceManager : IManagerBase
    {
		private static Dictionary<string, AssetBundle> cachedAsset = new Dictionary<string, AssetBundle>();
        public static Dictionary<string, string> AssetGuidDic = new Dictionary<string, string>();
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

		public static T GetResource<T>(string path) where T : Object //어지간하면 사용하지 맙시다.
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
