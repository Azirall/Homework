namespace ConsoleApp
{
    public class Weapon : Item, IEquippable
    {
        public int AdditionalDamage {get; private set; }

        public Weapon(string name, int price, int additionalDamage)
        {
            AdditionalDamage = additionalDamage;
            Price = price;
            Name = name;
            
        }

        public void Equip(Player player)
        {
            player.AddAdditionalDamage(AdditionalDamage);
        }

        public void Unequip(Player player)
        {
            player.RemoveAdditionalDamage(AdditionalDamage);
        }
    }
}


