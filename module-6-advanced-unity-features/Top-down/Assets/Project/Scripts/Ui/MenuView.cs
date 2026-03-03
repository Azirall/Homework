using TMPro;
using UnityEngine;

public class MenuView : MonoBehaviour, IEventUser
{
    [SerializeField] private GameObject _menuRoot;
    [SerializeField] private GameObject _menuContinueButton;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private TextMeshProUGUI _stateText;
    private EventBus _eventBus;

    public void Subscribe(EventBus bus)
    {
        _eventBus = bus;
        _eventBus.OnGameEvent += HandleEvent;
    }

    public void Unsubscribe()
    {
        if (_eventBus == null)
        {
            return;
        }

        _eventBus.OnGameEvent -= HandleEvent;
        _eventBus = null;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is not GameStateChanged stateChanged)
        {
            return;
        }

        UpdateView(stateChanged.State);
    }

    private void SetStateText(string text)
    {
        if (_stateText != null)
        {
            _stateText.text = text;
        }
    }

    private void UpdateView(GameStateType state)
    {
        switch (state)
        {
            case GameStateType.Start:
                _menuRoot.SetActive(true);
                _menuContinueButton.SetActive(false);
                _startButton.SetActive(true);
                _restartButton.SetActive(false);
                SetStateText("New game");
                break;

            case GameStateType.Playing:
                _menuRoot.SetActive(false);
                _menuContinueButton.SetActive(false);
                _startButton.SetActive(false);
                _restartButton.SetActive(false);
                SetStateText(string.Empty);
                break;

            case GameStateType.Pause:
                _menuRoot.SetActive(true);
                _menuContinueButton.SetActive(true);
                _startButton.SetActive(false);
                _restartButton.SetActive(true);
                SetStateText("Pause");
                break;

            case GameStateType.Win:
                _menuRoot.SetActive(true);
                _menuContinueButton.SetActive(false);
                _startButton.SetActive(false);
                _restartButton.SetActive(true);
                SetStateText("Win");
                break;

            case GameStateType.Lose:
                _menuRoot.SetActive(true);
                _menuContinueButton.SetActive(false);
                _startButton.SetActive(false);
                _restartButton.SetActive(true);
                SetStateText("Lose");
                break;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnValidate()
    {
        if (_menuRoot == null)
        {
            Debug.LogError($"MenuView: menu root is not assigned on {name}");
        }

        if (_menuContinueButton == null)
        {
            Debug.LogError($"MenuView: continue button is not assigned on {name}");
        }

        if (_startButton == null)
        {
            Debug.LogError($"MenuView: start button is not assigned on {name}");
        }

        if (_restartButton == null)
        {
            Debug.LogError($"MenuView: restart button is not assigned on {name}");
        }

        if (_stateText == null)
        {
            Debug.LogError($"MenuView: state text is not assigned on {name}");
        }
    }
}
