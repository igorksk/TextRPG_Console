namespace TextRPG.Models
{
    public class Enemy(string name, string description, int health, int minDamage, int maxDamage, int xpReward, int radiationOnHit = 0)
    {
        public string Name { get; init; } = name;
        public string Description { get; init; } = description;
        public int Health { get; set; } = health;
        public int MinDamage { get; init; } = minDamage;
        public int MaxDamage { get; init; } = maxDamage;
        public int XPReward { get; init; } = xpReward;
        public int RadiationOnHit { get; init; } = radiationOnHit;

        public int GetAttackDamage(Random rng)
        {
            return rng.Next(MinDamage, MaxDamage + 1);
        }

        public static class Data
        {
            public static readonly Enemy[] Enemies =
            [
                new("Radscorpion", "A large mutated scorpion with a poisonous sting.", 40, 6, 14, 40, 5),
                new("Mutant Raider", "A desperate human scavenger, aggressive and armed.", 30, 4, 10, 30, 0),
                new("Feral Hound", "A wild, irradiated dog that attacks in packs.", 25, 3, 9, 20, 2)
            ];
        }
    }
}
