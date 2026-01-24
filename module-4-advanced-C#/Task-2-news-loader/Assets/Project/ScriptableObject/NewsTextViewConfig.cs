using UnityEngine;

[CreateAssetMenu(fileName = "NewsTextViewConfig", menuName = "News/View/Text Config")]
public sealed class NewsTextViewConfig : ScriptableObject
{
    [Header("Fallback texts")]
    [SerializeField] private string _noTitleText = "[No title]";
    [SerializeField] private string _noContentText = "[No content]";

    [Header("Formatting")]
    [SerializeField] private string _dateFormat = "dd.MM.yyyy HH:mm";
    [SerializeField, Min(0)] private int _emptyLineCountAfterItem = 1;

    public string NoTitleText => _noTitleText;
    public string NoContentText => _noContentText;
    public string DateFormat => _dateFormat;
    
    public int EmptyLineCountAfterItem => _emptyLineCountAfterItem;
}
