using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
	/// <summary>
	/// 스파인을 컨트롤 하는 클래스
	/// </summary>
	/// <typeparam name="Anim">Spine Animation Enum</typeparam>
	public abstract class SpineController<Anim> : MonoBehaviour, ISpineController where Anim : System.Enum
    {
		public delegate Anim NameToType(string type);
		public delegate string TypeToName(Anim type);
		public delegate bool TypeToLoop(Anim type);
		protected TypeToName GetTypeToName { get; set; } = null;
		protected NameToType GetNameToType { get; set; } = null;
		protected TypeToLoop GetTypeToLoop { get; set; } = null;
		public Anim Animation { get; protected set; } = default;
		public abstract eSpineType SpineType { get; }

		[SerializeField] protected GameObject spineObj = null;
		protected SkeletonData skData = null;
		protected virtual void Awake()
		{
			InitializeComponent();
			InitializeTypeFunc();
		}
		protected virtual void Start()
		{
			InitializeStart();
		}
		public abstract void InitializeStart();
		public abstract void InitializeTypeFunc();
		public abstract void InitializeComponent();
		protected abstract TrackEntry SetAnimation(int trackIndex, string animName, bool loop);
		public abstract void SetDataAsset(SkeletonDataAsset asset);
		public abstract void SetSkin(string skinName);
		public abstract void SetShadow(bool show);
		public Transform SpineTransform
		{
			get => spineObj == null ? null : spineObj.transform;
		}
	}
}