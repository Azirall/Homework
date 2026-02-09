using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _logText;

    public void SetVisible(bool visible)
    {
        if (_canvasGroup == null)
            return;

        _canvasGroup.alpha = visible ? 1f : 0f;
    }

    public void SetLogs(IReadOnlyList<string> logs)
    {
        if (_logText == null || logs == null)
            return;

        _logText.text = string.Join("\n", logs);
    }
}
