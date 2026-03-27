using TMPro;
using UnityEngine;

public class GameplayHudView : MonoBehaviour, IGameplayHud
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _currentInputModeText;

    public void SetHealth(int health)
    {
        _healthText.text = $"health: {health}";
        Debug.Log($"health: {health}");
    }

    public void SetScore(int score)
    {
        _scoreText.text = $"score: {score}";
    }

    public void SetCurrentInputMode(InputSourceKind inputSourceKind)
    {
        _currentInputModeText.text = inputSourceKind.ToString();
    }

    private void OnValidate()
    {
        if (_healthText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_healthText)} to be assigned.", this);

        if (_scoreText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_scoreText)} to be assigned.", this);

        if (_currentInputModeText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_currentInputModeText)} to be assigned.", this);
    }
}
