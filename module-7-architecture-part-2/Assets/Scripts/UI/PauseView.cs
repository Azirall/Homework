using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour, IPauseView
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _toMenuButton;

    public event Action PauseRequested;
    public event Action ResumeRequested;
    public event Action ToMenuRequested;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _toMenuButton.onClick.AddListener(OnToMenuButtonClicked);
    }

    private void OnDestroy()
    {
        if (_pauseButton != null)
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);

        if (_resumeButton != null)
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);

        if (_toMenuButton != null)
            _toMenuButton.onClick.RemoveListener(OnToMenuButtonClicked);
    }

    private void OnPauseButtonClicked() => PauseRequested?.Invoke();
    private void OnResumeButtonClicked() => ResumeRequested?.Invoke();
    private void OnToMenuButtonClicked() => ToMenuRequested?.Invoke();

    void IPauseView.Resume() => ResumeRequested?.Invoke();
    void IPauseView.ToMenu() => ToMenuRequested?.Invoke();

    public void ShowPanel() => _panel.SetActive(true);
    public void HidePanel() => _panel.SetActive(false);

    private void OnValidate()
    {
        if (_panel == null)
            Debug.LogError($"{nameof(PauseView)} requires {nameof(_panel)} to be assigned.", this);

        if (_pauseButton == null)
            Debug.LogError($"{nameof(PauseView)} requires {nameof(_pauseButton)} to be assigned.", this);

        if (_resumeButton == null)
            Debug.LogError($"{nameof(PauseView)} requires {nameof(_resumeButton)} to be assigned.", this);

        if (_toMenuButton == null)
            Debug.LogError($"{nameof(PauseView)} requires {nameof(_toMenuButton)} to be assigned.", this);
    }
}
