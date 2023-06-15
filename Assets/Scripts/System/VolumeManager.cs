using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DOW
{
    public class VolumeManager : IManagerBase
    {
        private static VolumeManager instance = null;
        public static VolumeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new VolumeManager();

                return instance;
            }
        }

        private Volume volume = null;

        public void Initialize()
        {
            volume = GameObject.FindObjectOfType<Volume>();
            Debug.Log("VolumeManager Initialize" + volume.ToString());
        }

        public void StartBlur()
        {
            Debug.Log("VolumeManager Blur");
        }

        public void ClearEffect()
        {

        }

        public void Update(float dt) {}
    }

}