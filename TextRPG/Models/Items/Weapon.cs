namespace TextRPG.Models.Items
{
    public class Weapon(string name, string description, int value, int damage) : Item(name, description, value)
    {
        public int Damage { get; private set; } = damage;
    }
}
