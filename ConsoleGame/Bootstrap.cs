namespace ConsoleApp
{

    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            
            ConsoleUI ui = new();
            Inventory inventory = new();
            Player player = new("Игрок",10,1);
            Enemy enemy = new("Кабан",10,1);
            GameDirector director = new GameDirector(player,enemy,ui,inventory);
            player.SetGameDirector(director);
            CreateItems(inventory);
            
            director.RunGame();
        }

        private static void CreateItems(Inventory inventory)
        {
            Weapon knife = new Weapon("кинжал", 5, 1);
            Weapon sword = new Weapon("меч", 10, 2);
            Weapon axe = new Weapon("топор", 12, 3);
            Potion smallHeal = new Potion("Малое зелье лечения", 5, 0, 1);
            Potion ragePotion = new Potion("Зелье ярости", 0, 3, 2);
            Potion hybridPotion = new Potion("Эликсир отваги", 3, 2, 3);
            
            inventory.AddItem(axe);
            inventory.AddItem(sword);
            inventory.AddItem(knife);
            inventory.AddItem(smallHeal);
            inventory.AddItem(ragePotion);
            inventory.AddItem(hybridPotion);
        }
    }
}