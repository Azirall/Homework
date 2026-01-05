using TMPro;
using UnityEngine;
using Zenject;

public class TargetStatsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalTargetTmp;
    [SerializeField] private TextMeshProUGUI _destroyedTargetTmp;
    [SerializeField] private TextMeshProUGUI _shotCountTmp;
    [SerializeField] private string _shotCountText = "Выстрелов всего: ";
    [SerializeField] private string _totalTargetText = "Целей всего: ";
    [SerializeField] private string _destroyedTargetText = "Уничтожено целей: ";

    private TargetStats _targetStats;

    [Inject]
    public void Construct(TargetStats targetStats)
    {
        _targetStats = targetStats;
    }

    private void OnEnable()
    {
        if (_targetStats == null)
        {
            return;
        }

        _targetStats.SpawnedTargetsChanged += ChangeTotalTargetText;
        _targetStats.DestroyedTargetsChanged += ChangeDestroyedTargetText;
        _targetStats.ShotsCountChanged += ChangeShotCountText;

        ChangeTotalTargetText(_targetStats.SpawnedTargets);
        ChangeDestroyedTargetText(_targetStats.DestroyedTargets);
        ChangeShotCountText(_targetStats.ShotsCount);
    }

    private void OnDisable()
    {
        if (_targetStats == null)
        {
            return;
        }

        _targetStats.SpawnedTargetsChanged -= ChangeTotalTargetText;
        _targetStats.DestroyedTargetsChanged -= ChangeDestroyedTargetText;
        _targetStats.ShotsCountChanged -= ChangeShotCountText;
    }

    private void ChangeTotalTargetText(int count)
    {
        _totalTargetTmp.text = $"{_totalTargetText}{count.ToString()}";
    }

    private void ChangeDestroyedTargetText(int count)
    {
        _destroyedTargetTmp.text =  $"{_destroyedTargetText}{count.ToString()}";
    }

    private void ChangeShotCountText(int count)
    {
        _shotCountTmp.text =  $"{_shotCountText}{count.ToString()}";
    }
}
