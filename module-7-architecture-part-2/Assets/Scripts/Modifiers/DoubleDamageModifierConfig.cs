using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDamageModifierConfig", menuName = "Game/Modifiers/Double Damage Config")]

public class DoubleDamageModifierConfig : GameModifierConfig
{
    [SerializeField] private int _damageMultiplier = 2;

    public int DamageMultiplier => _damageMultiplier;

    protected override string BuildDefaultHudLine() => $"Dmg ×{DamageMultiplier}";
}
