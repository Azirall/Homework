using TMPro;
using UnityEngine;

public class LogItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetLogText(string logLine)
    {
        _text.text = logLine;
    }
}
