namespace TextRPG
{
    public class World
    {
        private readonly Random random;
        private List<Location> locations = [];

        public World()
        {
            random = new Random();
            InitializeLocations();
        }

        private void InitializeLocations()
        {
            locations =
            [
                new Location("Ruined City", "Ruins of a once-thriving metropolis"),
                new Location("Desert", "Endless sands where fertile land once was"),
                new Location("Abandoned Military Base", "Remnants of a military complex"),
                new Location("Underground Shelter", "A protected place from radiation"),
                new Location("Radioactive Zone", "Dangerous area with high radiation levels")
            ];
        }

        public void Explore(Player player)
        {
            Location currentLocation = locations[random.Next(locations.Count)];
            Console.WriteLine($"\nYou are at: {currentLocation.Name}");
            Console.WriteLine(currentLocation.Description);

            // Random events
            int eventRoll = random.Next(100);
            
            if (eventRoll < 30)
            {
                FindItem(player);
            }
            else if (eventRoll < 60)
            {
                EncounterEnemy(player);
            }
            else if (eventRoll < 80)
            {
                FindResources(player);
            }
            else
            {
                Console.WriteLine("You didn't find anything interesting.");
            }

            // Reduce resources when exploring
            player.ConsumeWater(10);
            player.ConsumeFood(10);
        }

        private void FindItem(Player player)
        {
            int itemType = random.Next(4);
            Item foundItem = itemType switch
            {
                0 => new Food("Canned Food", "Old canned food, but still edible", 10, 30),
                1 => new Water("Bottle of Water", "Dirty water, but drinkable", 5, 20),
                2 => new Weapon("Rusty Knife", "An old knife, but can still deal damage", 15, 10),
                _ => new Medicine("First Aid Kit", "A set for first aid", 20, 30, 10),
            };
            player.AddItem(foundItem);
        }

        private void EncounterEnemy(Player player)
        {
            Console.WriteLine("You encountered a mutated creature!");
            int damage = random.Next(5, 20);
            player.TakeDamage(damage);
            Console.WriteLine($"You took {damage} damage!");
        }

        private void FindResources(Player player)
        {
            int water = random.Next(5, 15);
            int food = random.Next(5, 15);
            
            player.AddWater(water);
            player.AddFood(food);
            
            Console.WriteLine($"You found {water} units of water and {food} units of food!");
        }
    }

    public class Location(string name, string description)
    {
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
    }
} 