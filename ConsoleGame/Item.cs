namespace ConsoleApp
{
    public abstract class Item
    {
        public string Name {get; protected set;}
        public int Price { get; protected set;}

        public virtual void Use() {}
    }
}


