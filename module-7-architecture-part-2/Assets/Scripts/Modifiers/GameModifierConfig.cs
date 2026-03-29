using UnityEngine;

public abstract class GameModifierConfig : ScriptableObject
{
    [SerializeField] private string _hudDescription;

    public string GetHudLine()
    {
        if (!string.IsNullOrWhiteSpace(_hudDescription))
            return _hudDescription.Trim();

        return BuildDefaultHudLine();
    }

    protected virtual string BuildDefaultHudLine() => string.Empty;
}
