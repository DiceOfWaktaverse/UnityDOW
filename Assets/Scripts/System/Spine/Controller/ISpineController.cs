using Spine.Unity;

namespace DOW
{
	public interface ISpineController
	{
		public eSpineType SpineType { get; }
		public void InitializeStart();
		public void InitializeTypeFunc();
		public void InitializeComponent();
		public void SetDataAsset(SkeletonDataAsset asset);
		public void SetSkin(string skinName);
		public void SetShadow(bool show);
	}
}