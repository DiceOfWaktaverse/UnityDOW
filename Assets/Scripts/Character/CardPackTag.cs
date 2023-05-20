namespace DOW
{
    [System.Flags]
    public enum eCardPackTag
    {
        NONE = 0,

        즉발 = 1 << 0,
        아이템 = 1 << 1,
        필드 = 1 << 2,
        컨텐츠 = 1 << 3,
        촬영 = 1 << 4,
        노래 = 1 << 5,

        //카드팩들 시작
        잔디의지배자 = 1 << 10,

    }
}