using UnityEngine;

[CreateAssetMenu(fileName = "RegenModifierConfig", menuName = "Game/Modifiers/Regen Config")]
public class RegenModifierConfig : GameModifierConfig
{
    [SerializeField] private float _timeBetweenRegenTicks = 1f;
    [SerializeField] private int _regenValue = 1;

    public float TimeBetweenRegenTicks => _timeBetweenRegenTicks;
    public int RegenValue => _regenValue;

    protected override string BuildDefaultHudLine() =>
        $"Regen +{RegenValue}/{TimeBetweenRegenTicks}s";
}
