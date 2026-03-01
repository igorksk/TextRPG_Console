namespace TextRPG.Items
{
    public class Item(string name, string description, int value)
    {
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public int Value { get; private set; } = value;

        public static class Data
        {
            public static readonly Food[] Foods =
            [
                new("Canned Food", "Old canned food, but still edible", 10, 30),
                new("Dried Meat", "Preserved jerky, high nutrition", 15, 40)
            ];

            public static readonly Water[] Waters =
            [
                new("Bottle of Water", "Dirty water, but drinkable", 5, 20),
                new("Purified Water", "Clean water, restores more hydration", 10, 40)
            ];

            public static readonly Weapon[] Weapons =
            [
                new("Rusty Knife", "An old knife, but can still deal damage", 15, 10),
                new("Pistol", "Old service pistol, needs ammo", 50, 25)
            ];

            public static readonly Medicine[] Medicines =
            [
                new("First Aid Kit", "A set for first aid", 20, 30, 10),
                new("Antirad Pack", "Removes radiation and heals minor wounds", 30, 20, 30)
            ];
        }
    }
}
