
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class TotalScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalScoreTmp;
    [SerializeField] private string text = "Рекорд : уничтожено целей - ";

    private SaveSystem _saveSystem;

    [Inject]
    public void Construct(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    private void Start()
    {
        int score = _saveSystem.Load(SaveKey.MaxDestroyedTargets);
        _totalScoreTmp.text = score == 0 ? "" : $"{text}{_saveSystem.Load(SaveKey.MaxDestroyedTargets)}";
    }
}
