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

        // Equipped weapon (null = hands)
        public Weapon? CurrentWeapon { get; private set; }

        public Player()
        {
            Health = 100;
            Radiation = 0;
            Water = 100;
            Food = 100;
            Inventory = [];

            Level = 1;
            Experience = 0;
            CurrentWeapon = null; // hands by default
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

        public void EquipWeapon(Weapon? weapon)
        {
            CurrentWeapon = weapon;
            Console.WriteLine(weapon == null ? "You are now unarmed (hands)." : $"Equipped: {weapon.Name}");
        }

        public void EquipWeaponFromInventory()
        {
            Console.WriteLine("\n=== Equip Weapon ===");
            Console.WriteLine("0. Hands (no weapon)");
            var weapons = new List<Weapon>();
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i] is Weapon w)
                {
                    weapons.Add(w);
                    Console.WriteLine($"{weapons.Count}. {w.Name} (Damage: {w.Damage})");
                }
            }

            if (weapons.Count == 0)
            {
                Console.WriteLine("No weapons in inventory.");
                return;
            }

            Console.WriteLine("Choose weapon number to equip (or 0 for hands):");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                if (choice == 0)
                {
                    EquipWeapon(null);
                    return;
                }
                if (choice >= 1 && choice <= weapons.Count)
                {
                    EquipWeapon(weapons[choice - 1]);
                    return;
                }
            }

            Console.WriteLine("Invalid choice. No changes made.");
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

            for (int i = 0; i < Inventory.Count; i++)
            {
                var item = Inventory[i];
                Console.WriteLine($"{i + 1}. {item.Name}: {item.Description}");
            }

            Console.WriteLine("\nChoose an item number to use/equip/drop or press ENTER to go back:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > Inventory.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            var selected = Inventory[choice - 1];
            UseItem(selected);
        }

        private void UseItem(Item item)
        {
            switch (item)
            {
                case Food f:
                    AddFood(f.Nutrition);
                    Inventory.Remove(item);
                    Console.WriteLine($"You ate {f.Name} and restored {f.Nutrition} food.");
                    break;
                case Water w:
                    AddWater(w.Hydration);
                    Inventory.Remove(item);
                    Console.WriteLine($"You drank {w.Name} and restored {w.Hydration} water.");
                    break;
                case Medicine m:
                    Health = Math.Min(100, Health + m.Healing);
                    Radiation = Math.Max(0, Radiation - m.RadiationRemoval);
                    Inventory.Remove(item);
                    Console.WriteLine($"You used {m.Name}. Healed {m.Healing} health and removed {m.RadiationRemoval} radiation.");
                    break;
                case Weapon wp:
                    Console.WriteLine($"Do you want to equip {wp.Name}? (y/n)");
                    var answer = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(answer) && answer.Trim().ToLower() == "y")
                    {
                        EquipWeapon(wp);
                    }
                    break;
                default:
                    Console.WriteLine("This item cannot be used.");
                    break;
            }
        }
    }
} 