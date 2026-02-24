using TMPro;
using UnityEngine;

public class MenuView : MonoBehaviour, IEventUser
{
    [SerializeField] private GameObject _menuRoot;
    [SerializeField] private GameObject _menuActionButton;
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
        if (_menuRoot == null)
        {
            return;
        }

        if (gameEvent is GameStateChanged { State: GameStateType.Start })
        {
            _menuRoot.SetActive(true);
            SetMenuActionButtonVisible(false);
            SetStateText(string.Empty);
            return;
        }

        if (gameEvent is GameStateChanged { State: GameStateType.Playing })
        {
            _menuRoot.SetActive(false);
            SetMenuActionButtonVisible(true);
            SetStateText(string.Empty);
            return;
        }

        if (gameEvent is GameStateChanged { State: GameStateType.Pause })
        {
            _menuRoot.SetActive(true);
            SetMenuActionButtonVisible(true);
            SetStateText("Pause");
            return;
        }

        if (gameEvent is GameStateChanged { State: GameStateType.Win } ||
            gameEvent is GameStateChanged { State: GameStateType.Lose })
        {
            _menuRoot.SetActive(true);
            SetMenuActionButtonVisible(false);
            SetStateText(gameEvent is GameStateChanged { State: GameStateType.Win } ? "Win" : "Lose");
        }
    }

    private void SetMenuActionButtonVisible(bool isVisible)
    {
        if (_menuActionButton != null)
        {
            _menuActionButton.SetActive(isVisible);
        }
    }

    private void SetStateText(string text)
    {
        if (_stateText != null)
        {
            _stateText.text = text;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
