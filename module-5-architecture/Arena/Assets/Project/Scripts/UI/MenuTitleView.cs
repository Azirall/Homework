using TMPro;
using UnityEngine;

public class MenuTitleView : MonoBehaviour
{
    [SerializeField] private string _loseText = "Lose";
    [SerializeField] private string _winText = "Win";
    [SerializeField] private string _pauseText  = "Pause";
    
    [SerializeField] private TextMeshProUGUI _titleText;

    public void ShowLoseText()
    {
        _titleText.text = _loseText;
    }

    public void ShowWinText()
    {
        _titleText.text = _winText;
    }

    public void ShowPauseText()
    {
        _titleText.text = _pauseText;
    }
}