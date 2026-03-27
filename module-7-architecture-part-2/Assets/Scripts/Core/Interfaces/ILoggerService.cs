public interface ILoggerService
{
    void GameStart();
    void StateTransition(string fromState, string toState);
    void InputSourceSwitched(InputSourceKind newSource);
    void ItemPickup();
    void DamageReceived(int amount);
    void HealReceived(int amount);
    void CoinCollected(int amount);
    void Death();
    void Restart();
}
