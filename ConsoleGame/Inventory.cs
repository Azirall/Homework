namespace ConsoleApp
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        
        private int _activeWeaponIndex;
        
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        
        public List<Weapon> GetWeaponsList()
        {
            List<Weapon> weapons = new();
            
            foreach (Item item in items)
            {
                if (item is Weapon)
                {
                    weapons.Add((Weapon)item);
                }
            }
            return weapons;
        }
        public List<Potion> GetPotionList()
        {
            List<Potion> potions = new();
            
            foreach (Item item in items)
            {
                if (item is Potion)
                {
                    potions.Add((Potion)item);
                }
            }
            return potions;
        }
        
    }
}


