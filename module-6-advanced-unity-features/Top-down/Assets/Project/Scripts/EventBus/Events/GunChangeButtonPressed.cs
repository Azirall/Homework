
public class GunChangeButtonPressed : IGameEvent
{
    public int Index {private set; get;}
    
    public GunChangeButtonPressed(int slotIndex)
    {
        Index = slotIndex;
    }
}