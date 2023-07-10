using UnityEngine;
using System.Collections;
using Spine.Unity;
using System;
using Spine;
using System.Collections.Generic;
using System.Linq;

namespace DOW
{
	/// <summary>
	/// 필요하다면 UI말고 SkeletonRenderer 용도의 스크립트 만들어서 사용하시면 됩니다.
	/// </summary>
	/// <typeparam name="Anim">해당 스파인의 애니메이션 정의 타입</typeparam>
	public abstract class UISpineController<Anim> : SpineController<Anim> where Anim : Enum
	{
		public override eSpineType SpineType { get => eSpineType.UI; }
		[SerializeField] protected SkeletonGraphic skeletonAni = null;
		public SkeletonGraphic SkeletonAni { get { return skeletonAni; } }
        #region Initialze
		public override void InitializeStart()
		{
			if (skeletonAni.skeletonDataAsset != null)
				skeletonAni.Initialize(true);

			SetShadow(false);
		}
		public override void InitializeComponent()
		{
			if (spineObj == null)
				spineObj = Func.GetChildrensByName(transform, "spine").gameObject;

			if (!spineObj.TryGetComponent(out skeletonAni))
				skeletonAni = spineObj.AddComponent<SkeletonGraphic>();
		}
		#endregion
		/// <summary>
		/// 외부 사용 용도 상속받아서 추가 구현
		/// </summary>
		/// <param name="anim">Spine Animation</param>
		/// <returns>Cur Track</returns>
		public virtual TrackEntry SetAnimation(Anim anim)
		{
			return SetAnimation(0, GetTypeToName(anim), GetTypeToLoop(anim));
		}
		/// <summary>
		/// 왠만하면 상속받아서도 고쳐서 사용하지 않는걸 추천.
		/// </summary>
		/// <param name="trackIndex">Spine 기본 파라메터1</param>
		/// <param name="animName">Spine 기본 파라메터2</param>
		/// <param name="loop">Spine 기본 파라메터3</param>
		/// <returns></returns>
		protected override TrackEntry SetAnimation(int trackIndex, string animName, bool loop)
		{
			if (skeletonAni == null || skeletonAni.AnimationState.GetCurrent(0).Animation.Name == animName)
				return null;

			return skeletonAni.AnimationState.SetAnimation(trackIndex, animName, loop);
		}
		/// <summary>
		/// 인게임 내에서 SkeletonData를 교체해야 할 경우(퍼포먼스가 심각하게 떨어지니 최대한 사용 자제)
		/// </summary>
		/// <param name="asset"></param>
		public override void SetDataAsset(SkeletonDataAsset asset)
		{
			if (asset == null)
				return;

			if (skeletonAni.skeletonDataAsset == asset)
				return;

			skeletonAni.initialSkinName = "default";
			skeletonAni.skeletonDataAsset = asset;
			skeletonAni.SkeletonDataAsset.Clear();
			skeletonAni.Clear();
			skeletonAni.Initialize(false);
			skeletonAni.AnimationState.ClearTracks();
			skData = skeletonAni.SkeletonDataAsset.GetSkeletonData(true);
		}
		public void MixAnim(string animName1, string animName2, float anim2Duration, float animMixDuration, float delay)
		{
			var spineAnimationState = skeletonAni.AnimationState;
			spineAnimationState.SetAnimation(0, animName1, true);
			spineAnimationState.SetEmptyAnimation(1, 0);
			spineAnimationState.AddAnimation(1, animName2, false, 0).MixDuration = anim2Duration;
			spineAnimationState.AddEmptyAnimation(1, animMixDuration, delay);
		}
		public SkeletonData GetSkeletonData()
		{
			if (skData == null)
				skData = skeletonAni.SkeletonDataAsset.GetSkeletonData(true);

			return skData;
		}
		public Spine.Animation GetAnimation(string anim)
		{
			var skData = GetSkeletonData();
			if (skData == null)
				return null;

			return skData.FindAnimation(anim);
		}
		/// <summary>
		/// 스킨을 사용한다면 사용하고, 아니라면 놔두면 됩니다.
		/// </summary>
		/// <param name="skinName"></param>
		public override void SetSkin(string skinName)
		{
			if (skeletonAni == null)
				return;

			skeletonAni.initialSkinName = skinName;
			skeletonAni.Skeleton.SetSkin(skinName);
			skeletonAni.Initialize(true);
		}
		/// <summary>
		/// 추후 툴 만들때 사용하거나 애니메이션 시간이 필요한 경우.
		/// </summary>
		/// <param name="anim"></param>
		/// <returns></returns>
		public virtual float GetAnimaionTime(Anim anim)
		{
			if (GetTypeToName == null)
				return 0f;

			var name = GetTypeToName(anim);

			var animation = GetAnimation(name);
			if (animation != null)
				return animation.Duration;

			return 0f;
		}
	}
}