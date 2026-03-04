namespace TextRPG.Models.Items
{
    public class Medicine(string name, string description, int value, int healing, int radiationRemoval) : Item(name, description, value)
    {
        public int Healing { get; private set; } = healing;
        public int RadiationRemoval { get; private set; } = radiationRemoval;
    }
}
