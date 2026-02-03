public struct ItemSpawned : IGameEvent
{
    public string ItemName {get; private set; }

   public ItemSpawned(string itemName)
    {
        ItemName = itemName;
    }
}