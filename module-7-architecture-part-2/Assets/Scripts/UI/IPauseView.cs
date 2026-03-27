using System;

public interface IPauseView
{
    event Action PauseRequested;
    event Action ResumeRequested;
    event Action ToMenuRequested;

    void Resume();
    void ToMenu();
    void ShowPanel();
    void HidePanel();
}
