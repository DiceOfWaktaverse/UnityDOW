namespace DOW
{
    public interface IPopup
    {
        int GetOrder();
        void Initialize();
        void Refresh();
        void ClosePopup();
        void Close();
        void SetActive(bool value);
    }
}
