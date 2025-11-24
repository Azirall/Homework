namespace ConsoleApp
{
    public class Potion : Item, IConsumable
    {
        public int HealPower { get; private set;}
        public int AdditionalDamage { get; private set;}
        public int Duration { get; private set;}

        public Potion(string name,int healPower, int additionalDamage, int timeOfAction)
        {
            Name = name;
            HealPower = healPower;
            Duration = timeOfAction;
            AdditionalDamage = additionalDamage;
        }

        public void Consume(Player player)
        {
            
        }
    }
}


