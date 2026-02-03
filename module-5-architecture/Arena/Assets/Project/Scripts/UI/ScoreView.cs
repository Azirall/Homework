using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
      [SerializeField] private TextMeshProUGUI _scoreText; 
      [SerializeField] private string _text = "Score";

      private void Start()
      {
            EventBus.OnGameEvent += HandleEvent;
      }

      private void HandleEvent(IGameEvent gameEvent)
      {
            if (gameEvent is ScoreChanged scoreEvent)
            {
                  _scoreText.text = $"{_text}: {scoreEvent.Score}";
            }
      }

      public void OnDestroy()
      {
            EventBus.OnGameEvent -= HandleEvent;
      }
}