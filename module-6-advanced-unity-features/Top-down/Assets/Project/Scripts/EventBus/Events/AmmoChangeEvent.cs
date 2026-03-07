public class AmmoChangeEvent : IGameEvent
{
    public int CurrentMagazineSize { get; private set; }

    public int MaxMagazineSize { get; private set; }

    public AmmoChangeEvent(int currentMagazineSize, int maxMagazineSize)
    {
        CurrentMagazineSize = currentMagazineSize;
        MaxMagazineSize = maxMagazineSize;
    }

    public void Set(int current, int max)
    {
        CurrentMagazineSize = current;
        MaxMagazineSize = max;
    }
}