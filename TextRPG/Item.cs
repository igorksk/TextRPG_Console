namespace TextRPG
{
    public class Item(string name, string description, int value)
    {
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public int Value { get; private set; } = value;
    }

    public class Food(string name, string description, int value, int nutrition) : Item(name, description, value)
    {
        public int Nutrition { get; private set; } = nutrition;
    }

    public class Water(string name, string description, int value, int hydration) : Item(name, description, value)
    {
        public int Hydration { get; private set; } = hydration;
    }

    public class Weapon(string name, string description, int value, int damage) : Item(name, description, value)
    {
        public int Damage { get; private set; } = damage;
    }

    public class Medicine(string name, string description, int value, int healing, int radiationRemoval) : Item(name, description, value)
    {
        public int Healing { get; private set; } = healing;
        public int RadiationRemoval { get; private set; } = radiationRemoval;
    }
} 