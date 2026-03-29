using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayHudView : MonoBehaviour, IGameplayHud
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _currentInputModeText;
    [SerializeField] private TextMeshProUGUI _modifiersText;

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

    public void SetActiveModifierLines(IReadOnlyList<string> lines)
    {
        if (lines == null || lines.Count == 0)
        {
            _modifiersText.text = "Mods: none";
            return;
        }

        var prefixed = new string[lines.Count];
        for (var i = 0; i < lines.Count; i++)
            prefixed[i] = "• " + lines[i];

        _modifiersText.text = "Mods:\n" + string.Join("\n", prefixed);
    }

    private void OnValidate()
    {
        if (_healthText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_healthText)} to be assigned.", this);

        if (_scoreText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_scoreText)} to be assigned.", this);

        if (_currentInputModeText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_currentInputModeText)} to be assigned.", this);

        if (_modifiersText == null)
            Debug.LogError($"{nameof(GameplayHudView)} requires {nameof(_modifiersText)} to be assigned.", this);
    }
}
