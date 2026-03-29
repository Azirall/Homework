using UnityEngine;

public class DebugLoggerService : ILoggerService
{
    public void GameStart()
    {
        Debug.Log("Game started");
    }

    public void StateTransition(string fromState, string toState)
    {
        Debug.Log($"[GameStateMachine] Transition: {fromState} -> {toState}");
    }

    public void InputSourceSwitched(InputSourceKind newSource)
    {
        Debug.Log($"Input source switched: {newSource}");
    }

    public void ItemPickup()
    {
        Debug.Log("Item picked up");
    }

    public void DamageReceived(int amount)
    {
        Debug.Log($"Damage received: {amount}");
    }

    public void HealReceived(int amount)
    {
        Debug.Log($"Heal received: {amount}");
    }

    public void CoinCollected(int amount)
    {
        Debug.Log($"Coin collected: {amount}");
    }

    public void Death()
    {
        Debug.Log("Player death");
    }

    public void Restart()
    {
        Debug.Log("Game restart");
    }

    public void GameModifierEnabled(string modifierName)
    {
        Debug.Log($"[GameModifier] {modifierName} enabled");
    }
}
