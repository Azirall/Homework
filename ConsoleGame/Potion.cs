namespace ConsoleApp
{
    public class Potion : Item, IConsumable
    {
        public int HealPower { get; private set; }
        public int DamageBuff { get; private set; }
        public int Duration { get; private set; }

        public Potion(string name, int healPower, int damageBuff, int duration)
        {
            Name = name;
            HealPower = healPower;
            DamageBuff = damageBuff;
            Duration = duration;
        }

        public void Consume(Player player)
        {
            if (DamageBuff > 0)
                player.AddDamageBuff(DamageBuff);
            
            player.SetActivePotion(this);
        }

        public void Tick(Player player)
        {
            if (HealPower > 0)
                player.Heal(HealPower);
        }

        public void RemoveBuff(Player player)
        {
            if (DamageBuff > 0)
                player.RemoveAdditionalDamage(DamageBuff);
        }
    }
}


