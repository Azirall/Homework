using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _uiItemPrefab;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private HorizontalLayoutGroup _layout;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;
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
        StartCoroutine(DisableLayoutDelayed());
        
    }

    private void OnValidate()
    {
        if (_uiItemPrefab == null)
        {
            Debug.LogError($"No UI Item Prefab assigned",this);
        }

        if (_itemContainer == null)
        {
            Debug.LogError($"No Item Container assigned",this);
        }

        if (_layout == null)
        {
            Debug.LogError($"No Layout assigned",this);
        }
    }
    private IEnumerator DisableLayoutDelayed()
    {
        yield return new WaitForEndOfFrame();

        _contentSizeFitter.enabled = false;
        _layout.enabled = false;
    }
}
