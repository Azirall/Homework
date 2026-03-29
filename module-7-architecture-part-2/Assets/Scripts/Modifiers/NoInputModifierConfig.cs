using UnityEngine;

[CreateAssetMenu(fileName = "NoInputModifierConfig", menuName = "Game/Modifiers/No Input Config")]
public class NoInputModifierConfig : GameModifierConfig
{
    [SerializeField] private float _disableDuration = 1f;
    [SerializeField] private float _timeBetweenReenables = 1f;

    public float DisableDuration => _disableDuration;
    public float TimeBetweenReenables => _timeBetweenReenables;

    protected override string BuildDefaultHudLine() =>
        $"Input lock {DisableDuration}s/{TimeBetweenReenables}s";
}
