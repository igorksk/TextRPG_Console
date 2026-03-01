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
            // Select a category: 0 = Food, 1 = Water, 2 = Weapon, 3 = Medicine
            int category = random.Next(4);
            Item foundItem = category switch
            {
                0 => Item.Data.Foods[random.Next(Item.Data.Foods.Length)],
                1 => Item.Data.Waters[random.Next(Item.Data.Waters.Length)],
                2 => Item.Data.Weapons[random.Next(Item.Data.Weapons.Length)],
                _ => Item.Data.Medicines[random.Next(Item.Data.Medicines.Length)],
            };

            player.AddItem(foundItem);
        }

        private void EncounterEnemy(Player player)
        {
            Console.WriteLine("You encountered a mutated creature!");

            int enemyHealth = random.Next(20, 61);
            Console.WriteLine($"Enemy health: {enemyHealth}");

            // Turn-based fight loop
            while (enemyHealth > 0)
            {
                Console.WriteLine($"\nYour Health: {player.Health} | Enemy Health: {enemyHealth}");

                // collect weapons from inventory
                var weapons = new List<Weapon>();
                foreach (var it in player.Inventory)
                {
                    if (it is Weapon w)
                        weapons.Add(w);
                }

                Console.WriteLine("Choose your action:");
                Console.WriteLine("1. Attack with hands");
                if (weapons.Count > 0)
                    Console.WriteLine("2. Attack with weapon");
                Console.WriteLine("3. Attempt to flee");

                string? input = Console.ReadLine();

                if (input == "1")
                {
                    int damage = random.Next(1, 6); // weak hand attack
                    enemyHealth -= damage;
                    Console.WriteLine($"You punch the creature for {damage} damage.");
                }
                else if (input == "2" && weapons.Count > 0)
                {
                    Console.WriteLine("Choose a weapon:");
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {weapons[i].Name} (Damage: {weapons[i].Damage})");
                    }
                    string? choice = Console.ReadLine();
                    if (int.TryParse(choice, out int idx) && idx >= 1 && idx <= weapons.Count)
                    {
                        var weapon = weapons[idx - 1];
                        int damage = weapon.Damage + random.Next(-2, 3);
                        damage = Math.Max(1, damage);
                        enemyHealth -= damage;
                        Console.WriteLine($"You attack with {weapon.Name} for {damage} damage.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid weapon choice. You lose your turn.");
                    }
                }
                else if (input == "3")
                {
                    // 50% chance to flee
                    if (random.Next(100) < 50)
                    {
                        Console.WriteLine("You managed to flee from the creature.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You failed to flee!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }

                if (enemyHealth <= 0)
                {
                    Console.WriteLine("You defeated the creature!");
                    break;
                }

                // Enemy retaliates if player attacked or failed to flee
                int enemyDamage = random.Next(5, 20);
                player.TakeDamage(enemyDamage);
                Console.WriteLine($"The creature attacks and deals {enemyDamage} damage to you.");
            }
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