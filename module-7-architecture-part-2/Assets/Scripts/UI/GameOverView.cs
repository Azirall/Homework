using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IGameOverView
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _toMenuButton;
    public event Action RestartRequested;

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDestroy()
    {
        if (_restartButton != null)
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked() => RestartRequested?.Invoke();


    public void RestartToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowPanel() => _panel.SetActive(true);
    public void HidePanel() => _panel.SetActive(false);

    private void OnValidate()
    {
        if (_panel == null)
            Debug.LogError($"{nameof(GameOverView)} requires {nameof(_panel)} to be assigned.", this);

        if (_restartButton == null)
            Debug.LogError($"{nameof(GameOverView)} requires {nameof(_restartButton)} to be assigned.", this);
    }
}
