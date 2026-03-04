namespace TextRPG.Models
{
    public class Location(string name, string description)
    {
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
    }
}
