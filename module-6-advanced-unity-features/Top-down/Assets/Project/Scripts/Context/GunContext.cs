using System.Collections.Generic;
using UnityEngine;

public class GunContext : MonoBehaviour
{ 
    [SerializeField] private List<GunConfig> _configs;

    public IReadOnlyList<GunConfig> GetConfigs()
    {
        return _configs;
    }
}
