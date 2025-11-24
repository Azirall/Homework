namespace ConsoleApp
{
    public interface IConsumable
    {
        void Consume(Player player);
        void Tick(Player player);      
        void RemoveBuff(Player player);
    }
}


