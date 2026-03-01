namespace TextRPG
{
    public class Player
    {
        public int Health { get; private set; }
        public int Radiation { get; private set; }
        public int Water { get; private set; }
        public int Food { get; private set; }
        public List<Item> Inventory { get; private set; }

        // Progression
        public int Level { get; private set; }
        public int Experience { get; private set; }

        public Player()
        {
            Health = 100;
            Radiation = 0;
            Water = 100;
            Food = 100;
            Inventory = [];

            Level = 1;
            Experience = 0;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Console.WriteLine("You died...");
                Environment.Exit(0);
            }
        }

        public void TakeRadiation(int amount)
        {
            Radiation += amount;
            if (Radiation >= 100)
            {
                Console.WriteLine("You received a lethal dose of radiation...");
                Environment.Exit(0);
            }
        }

        public void ConsumeWater(int amount)
        {
            Water -= amount;
            if (Water <= 0)
            {
                Console.WriteLine("You died of dehydration...");
                Environment.Exit(0);
            }
        }

        public void ConsumeFood(int amount)
        {
            Food -= amount;
            if (Food <= 0)
            {
                Console.WriteLine("You died of starvation...");
                Environment.Exit(0);
            }
        }

        public void AddWater(int amount)
        {
            Water = Math.Min(100, Water + amount);
        }

        public void AddFood(int amount)
        {
            Food = Math.Min(100, Food + amount);
        }

        public void Rest()
        {
            Health = Math.Min(100, Health + 20);
            Console.WriteLine("You rested and recovered some health.");
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"You found: {item.Name}");
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0) return;
            Experience += amount;
            Console.WriteLine($"You gained {amount} XP.");

            // Level up while enough XP
            while (Experience >= Level * 100)
            {
                Experience -= Level * 100;
                Level++;
                // reward on level up
                Health = Math.Min(100, Health + 20);
                Console.WriteLine($"You leveled up! You are now level {Level}. Health restored slightly.");
            }
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n=== Inventory ===");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item.Name}: {item.Description}");
            }
        }
    }
} 