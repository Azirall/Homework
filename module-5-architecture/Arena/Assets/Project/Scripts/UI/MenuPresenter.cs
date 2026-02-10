using UnityEngine;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private MenuTitleView _menuTitleView;
    [SerializeField] private GameObject _continueButton;
    private void Start()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(IGameEvent gameEvent)
    {
        if (gameEvent is GameStateChanged state)
        {
            _canvasGroup.alpha = state.GameState is GameState.Paused or GameState.Win or GameState.Lose ? 1 : 0;
            _continueButton.SetActive(state.GameState == GameState.Paused);
            
            switch (state.GameState)
            {
                case GameState.Paused:
                    _menuTitleView.ShowPauseText();
                    break;
                case GameState.Win:
                    _menuTitleView.ShowWinText();
                    break;
                case GameState.Lose:
                    _menuTitleView.ShowLoseText();
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }
}
