public interface IGameplayHud
{
    void SetHealth(int health);
    void SetScore(int score);
    void SetCurrentInputMode(InputSourceKind inputSourceKind);
}
