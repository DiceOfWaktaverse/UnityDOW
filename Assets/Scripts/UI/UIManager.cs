using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class UIManager : IManagerBase
    {
        private static UIManager instance = null;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIManager();
                }
                return instance;
            }
        }

        private UIBeacon beacon = null;
        public UIBeacon Beacon 
        {
            get 
            {
                return beacon;
            }
        }

        private Camera uiCamera = null;
        public Camera UICamera
        {
            get
            {
                if (uiCamera == null)
                {
                    var camera = GameObject.FindGameObjectWithTag("UICamera");
                    if (camera != null)
                        uiCamera = camera.GetComponent<Camera>();
                }
                return uiCamera;
            }
        }

        public Queue<eSceneType> refreshQueue = new Queue<eSceneType>();

        public eSceneType CurrentUIType { get; private set; } = eSceneType.NONE;

        public void Initialize()
        {
            if (Beacon != null && Beacon.UIObjects != null)
            {
                var count = Beacon.UIObjects.Count;
                for(var i = 0; i < count; ++i)
                {
                    var obj = Beacon.UIObjects[i];
                    if (obj == null)
                        continue;
                    obj.Initialize();
                }
            }
        }

        public void InitUI(eSceneType type)
        {
            CurrentUIType = type;
            if (Beacon != null && Beacon.UIObjects != null)
            {
                var count = Beacon.UIObjects.Count;
                for (var i = 0; i < count; ++i)
                {
                    var obj = Beacon.UIObjects[i];
                    if (obj == null)
                        continue;
                    obj.InitializeUI(type);
                }
            }
        }

        public void RefreshUI()
        {
            if (Beacon != null && Beacon.UIObjects != null)
            {
                var count = Beacon.UIObjects.Count;
                for (var i = 0; i < count; ++i)
                {
                    var obj = Beacon.UIObjects[i];
                    if (obj == null)
                        continue;
                    obj.RefreshUI();
                }
            }
        }

        public void RefreshUI(eSceneType type)
        {
            if (Beacon != null && Beacon.UIObjects != null)
            {
                var count = Beacon.UIObjects.Count;
                for (var i = 0; i < count; ++i)
                {
                    var obj = Beacon.UIObjects[i];
                    if (obj == null)
                        continue;
                    obj.RefreshUI(type);
                }
            }
            else
            {
                refreshQueue.Enqueue(type);
            }
        }

        public void RefreshCurrentUI()
        {
            if (Beacon != null && Beacon.UIObjects != null)
            {
                var count = Beacon.UIObjects.Count;
                for (var i = 0; i < count; ++i)
                {
                    var obj = Beacon.UIObjects[i];
                    if (obj == null)
                        continue;
                    obj.RefreshUI(CurrentUIType);
                }
            }
        }

        public void Update(float dt) { }

        public void SetBeacon(UIBeacon beacon)
        {
            if (beacon == null)
                return;

            this.beacon = beacon;
            Initialize();
            InitUI(CurrentUIType);

            while(refreshQueue.Count > 0)
            {
                RefreshUI(refreshQueue.Dequeue());
            }
        }

        public static IEnumerator InitUICoroutine(eSceneType type)
        {
            Instance.InitUI(type);
            yield break;
        }

        public static IEnumerator RefreshUICoroutine(eSceneType type)
        {
            Instance.RefreshUI(type);
            yield break;
        }
    }
}