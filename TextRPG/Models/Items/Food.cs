namespace TextRPG.Models.Items
{
    public class Food(string name, string description, int value, int nutrition) : Item(name, description, value)
    {
        public int Nutrition { get; private set; } = nutrition;
    }
}
