namespace DOW
{
    public delegate void VoidDelegate();
	public static class Define
	{
        public static readonly int Day = 86400;
        public static readonly int Hour = 3600;
        public static readonly int Minute = 60;

        public static eCardPackTag ConvertCardTag(string value)
        {
            switch (value)
            {
                case "즉발":
                    return eCardPackTag.즉발;
                case "아이템":
                    return eCardPackTag.아이템;
                case "필드":
                    return eCardPackTag.필드;
                case "컨텐츠":
                    return eCardPackTag.컨텐츠;
                case "촬영":
                    return eCardPackTag.촬영;
                case "노래":
                    return eCardPackTag.노래;
                case "잔디의 지배자":
                    return eCardPackTag.잔디의지배자;
                default:
                    return eCardPackTag.NONE;
            }
        }
	}
}