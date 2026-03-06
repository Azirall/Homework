using System.Text;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour, IEventUser
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private readonly StringBuilder _textBuilder = new StringBuilder(32);
    private EventBus _eventBus;
    
    public void Subscribe(EventBus eventBus)
    {
        _eventBus = eventBus;
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
        if (gameEvent is ScoreChanged scoreEvent)
        {
            UpdateText(scoreEvent.Score);
        }
    }
    private void UpdateText(string text)
    {
        _textBuilder.Clear();
        _textBuilder.Append("Score: ");
        _textBuilder.Append(text);
        _scoreText.text = _textBuilder.ToString();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
