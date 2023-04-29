namespace DOW
{

    [System.Flags]
    public enum eSceneType
    {
        None = 0,

        Intro = 1,
    }
    public interface IUIBase
    {
        void Initialize();
        void InitializeUI(eSceneType uiType);
        void RefreshUI();
        void ShowEvent();
        void HideEvent();
    }
}