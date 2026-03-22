using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    public event Action StartRequested;
    public event Action ExitRequested;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy()
    {
        if (_startButton != null)
            _startButton.onClick.RemoveListener(OnStartButtonClicked);

        if (_exitButton != null)
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked() => StartRequested?.Invoke();
    private void OnExitButtonClicked() => ExitRequested?.Invoke();

    void IMainMenuView.Start() => StartRequested?.Invoke();
    void IMainMenuView.Exit() => ExitRequested?.Invoke();

    public void ShowPanel() => _panel.SetActive(true);
    public void HidePanel() => _panel.SetActive(false);

    private void OnValidate()
    {
        if (_panel == null)
            Debug.LogError($"{nameof(MainMenuView)} requires {nameof(_panel)} to be assigned.", this);

        if (_startButton == null)
            Debug.LogError($"{nameof(MainMenuView)} requires {nameof(_startButton)} to be assigned.", this);

        if (_exitButton == null)
            Debug.LogError($"{nameof(MainMenuView)} requires {nameof(_exitButton)} to be assigned.", this);
    }
}
