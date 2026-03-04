namespace TextRPG.Models.Items
{
    public class Water(string name, string description, int value, int hydration) : Item(name, description, value)
    {
        public int Hydration { get; private set; } = hydration;
    }
}
