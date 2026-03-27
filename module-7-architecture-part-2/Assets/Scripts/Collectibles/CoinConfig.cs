using UnityEngine;

[CreateAssetMenu(fileName = "CoinConfig", menuName = "Game/Coin Config")]
public class CoinConfig : ScriptableObject
{
    [SerializeField] private int _value = 1;

    public int Value => _value;
}
