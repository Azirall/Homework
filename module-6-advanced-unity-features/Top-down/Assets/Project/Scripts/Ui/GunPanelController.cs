
using System;
using System.Collections.Generic;
using UnityEngine;

public class GunPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _uiItemPrefab;
    [SerializeField] private Transform _itemContainer;
    public void Init(GunContext gunContext)
    {
        UpdateUi(gunContext.GetConfigs());
    }

    private void UpdateUi(IReadOnlyList<GunConfig> gunList)
    {
        var i = 1;
        
        foreach (var gun in gunList)
        {
           var item = Instantiate(_uiItemPrefab, _itemContainer).GetComponent<GunItemView>();
           item.Init(gun.name, i);
           i++;
        }
    }

    private void OnValidate()
    {
        if (_uiItemPrefab == null)
        {
            Debug.LogError($"No UI Item Prefab in {name} assigned");
        }

        if (_itemContainer == null)
        {
            Debug.LogError($"No Item Container in {name} assigned");
        }
    }
}
