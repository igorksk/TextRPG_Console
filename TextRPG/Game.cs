namespace TextRPG
{
    public class Game
    {
        private readonly Player player;
        private readonly World world;
        private bool isRunning;

        public Game()
        {
            player = new Player();
            world = new World();
            isRunning = true;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the post-apocalyptic world!");
            Console.WriteLine("You wake up in a ruined shelter...");
            
            while (isRunning)
            {
                DisplayStatus();
                ProcessInput();
            }
        }

        private void DisplayStatus()
        {
            Console.WriteLine("\n=== Status ===");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Radiation: {player.Radiation}");
            Console.WriteLine($"Water: {player.Water}");
            Console.WriteLine($"Food: {player.Food}");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Explore");
            Console.WriteLine("2. Rest");
            Console.WriteLine("3. Check inventory");
            Console.WriteLine("4. Exit game");
        }

        private void ProcessInput()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    world.Explore(player);
                    break;
                case "2":
                    player.Rest();
                    break;
                case "3":
                    player.ShowInventory();
                    break;
                case "4":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }
} 