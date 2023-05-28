namespace DOW
{

    [System.Flags]
    public enum eSceneType
    {
        NONE = 0,
        START,

        INTRO = START,
        MAIN_MENU,

        MAX
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