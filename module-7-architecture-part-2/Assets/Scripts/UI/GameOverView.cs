using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IGameOverView
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _toMenuButton;
    public event Action RestartRequested;
    public event Action ToMenuRequested;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _toMenuButton.onClick.AddListener(OnToMenuButtonClicked);
    }

    private void OnDestroy()
    {
        if (_restartButton != null)
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);

        if (_toMenuButton != null)
            _toMenuButton.onClick.RemoveListener(OnToMenuButtonClicked);
    }

    private void OnRestartButtonClicked() => RestartRequested?.Invoke();
    private void OnToMenuButtonClicked() => ToMenuRequested?.Invoke();

    public void Restart() => RestartRequested?.Invoke();

    public void ToMenu() => ToMenuRequested?.Invoke();

    public void ShowPanel() => _panel.SetActive(true);
    public void HidePanel() => _panel.SetActive(false);

    private void OnValidate()
    {
        if (_panel == null)
            Debug.LogError($"{nameof(GameOverView)} requires {nameof(_panel)} to be assigned.", this);

        if (_restartButton == null)
            Debug.LogError($"{nameof(GameOverView)} requires {nameof(_restartButton)} to be assigned.", this);

        if (_toMenuButton == null)
            Debug.LogError($"{nameof(GameOverView)} requires {nameof(_toMenuButton)} to be assigned.", this);
    }
}
