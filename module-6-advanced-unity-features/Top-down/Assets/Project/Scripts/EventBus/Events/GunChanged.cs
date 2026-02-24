public class GunChanged : IGameEvent
{
    public GunConfig GunConfig { get; private set;}
    
    public GunChanged(GunConfig config)
    {
        GunConfig =  config;
    }
}