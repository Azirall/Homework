using System;
using TMPro;
using UnityEngine;

public class TargetStatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalTargetTmp;
    [SerializeField] private TextMeshProUGUI _destroyedTargetTmp;
    [SerializeField] private TextMeshProUGUI _shotCountTmp;
    [SerializeField] private string _shotCountText = "Выстрелов всего: ";
    [SerializeField] private string _totalTargetText = "Целей всего: ";
    [SerializeField] private string _destroyedTargetText = "Уничтожено целей: ";


    public void ChangeTotalTargetText(int count)
    {
        _totalTargetTmp.text = $"{_totalTargetText}{count.ToString()}";
    }

    public void ChangeDestroyedTargetText(int count)
    {
        _destroyedTargetTmp.text =  $"{_destroyedTargetText}{count.ToString()}";
    }

    public void ChangeShotCountText(int count)
    {
       _shotCountTmp.text =  $"{_shotCountText}{count.ToString()}";
    }
}
