using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    [System.Serializable]
    public class SceneFadeData
    {
        [SerializeField]
        protected eLoadingEffectType type = eLoadingEffectType.FADE_LOGO;
        [SerializeField]
        protected GameObject target = null;
        public eLoadingEffectType Type { get => type; }
        public GameObject Target { get => target; }
    }
    public class LoadingBeacon : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup group = null;
        public CanvasGroup Group { get { return group; } }
        [SerializeField]
        private List<SceneFadeData> effectBeacon = null;
        public List<SceneFadeData> Beacon { get { return effectBeacon; } }

        protected virtual void Awake()
        {
            LoadingManager.Instance.SetBeacon(Group, Beacon);
        }
    }
}