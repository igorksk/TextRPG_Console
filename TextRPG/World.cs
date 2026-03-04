using TextRPG.Models;
using TextRPG.Models.Items;

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
            
            if (eventRoll < 40)
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
            // Randomly select an enemy type
            Enemy baseEnemy = Enemy.Data.Enemies[random.Next(Enemy.Data.Enemies.Length)];
            // Clone the enemy so we can mutate health
            Enemy enemy = new(baseEnemy.Name, baseEnemy.Description, baseEnemy.Health, baseEnemy.MinDamage, baseEnemy.MaxDamage, baseEnemy.XPReward, baseEnemy.RadiationOnHit);

            Console.WriteLine($"You encountered a {enemy.Name}!");
            Console.WriteLine(enemy.Description);
            Console.WriteLine($"Enemy health: {enemy.Health}");

            // Turn-based fight loop
            while (enemy.Health > 0)
            {
                Console.WriteLine($"\nYour Health: {player.Health} | {enemy.Name} Health: {enemy.Health}");
                Console.WriteLine($"Level: {player.Level} | XP: {player.Experience}/{player.Level * 100}");
                Console.WriteLine($"Equipped: {(player.CurrentWeapon == null ? "Hands" : player.CurrentWeapon.Name)}");

                Console.WriteLine("Choose your action:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Equip weapon");
                Console.WriteLine("3. Attempt to flee");

                string? input = Console.ReadLine();

                bool playerDidAttack = false;

                if (input == "1")
                {
                    int damage;
                    if (player.CurrentWeapon == null)
                    {
                        damage = random.Next(1, 6); // hands
                        enemy.Health -= damage;
                        Console.WriteLine($"You attack with your hands for {damage} damage.");
                    }
                    else
                    {
                        var weapon = player.CurrentWeapon;
                        damage = weapon.Damage + random.Next(-2, 3);
                        damage = Math.Max(1, damage);
                        enemy.Health -= damage;
                        Console.WriteLine($"You attack with {weapon.Name} for {damage} damage.");
                    }
                    playerDidAttack = true;
                }
                else if (input == "2")
                {
                    player.EquipWeaponFromInventory();
                }
                else if (input == "3")
                {
                    // 50% chance to flee
                    if (random.Next(100) < 50)
                    {
                        Console.WriteLine($"You managed to flee from the {enemy.Name}.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"You failed to flee!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action.");
                }

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {enemy.Name}!");
                    player.AddExperience(enemy.XPReward);
                    break;
                }

                // Enemy retaliates if player attacked or failed to flee
                if (playerDidAttack || input == "3")
                {
                    int enemyDamage = enemy.GetAttackDamage(random);
                    player.TakeDamage(enemyDamage);
                    Console.WriteLine($"The {enemy.Name} attacks and deals {enemyDamage} damage to you.");
                    if (enemy.RadiationOnHit > 0)
                    {
                        player.TakeRadiation(enemy.RadiationOnHit);
                        Console.WriteLine($"You received {enemy.RadiationOnHit} radiation from the {enemy.Name}!");
                    }
                }
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
} 