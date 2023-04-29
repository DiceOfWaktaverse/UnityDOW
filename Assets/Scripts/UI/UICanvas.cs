using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class UICanvas : PersistentSingleton<UICanvas>
    {
        [SerializeField] Canvas uiMainCanvas = null;
        [SerializeField] Camera uiMainCamera = null;

        public Camera GetCamera()
        {
            return uiMainCamera;
        }
        public RectTransform GetCanvasRectTransform()
        {
            return uiMainCanvas.GetComponent<RectTransform>();
        }
        public Rect GetCanvasRect()
        {
            return uiMainCanvas.pixelRect;
        }
    }
}