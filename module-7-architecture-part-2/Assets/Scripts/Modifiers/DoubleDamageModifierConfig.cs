using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDamageModifierConfig", menuName = "Game/Modifiers/Double Damage Config")]
public class DoubleDamageModifierConfig : GameModifierConfig
{
    [SerializeField] private float _damageMultiplier = 2f;

    public float DamageMultiplier => _damageMultiplier;
}
