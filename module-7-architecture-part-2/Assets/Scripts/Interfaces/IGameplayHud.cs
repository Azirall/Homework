public interface IGameplayHud
{
    int Health { get; set; }
    int Score { get; set; }
    InputSourceKind CurrentInputMode { get; set; }
}
