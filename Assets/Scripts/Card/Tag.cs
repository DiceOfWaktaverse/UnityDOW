
namespace DOW
{
    public class Tag
    {
        public string Key { get; protected set; } = "";
        public string Slug { get; protected set; } = "";
        public string Label { get; protected set; } = "";
        public string Description { get; protected set; } = "";

        public Tag(string key)
        {
            TagData datum = TagData.Get(key);
            Key = key;
            Slug = datum.Slug;
            Label = datum.Label;
            Description = datum.Description;
        }
    }
}
