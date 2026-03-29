using System;

public interface IGameOverView
{
    event Action RestartRequested;
    event Action ToMenuRequested;

    void Restart();
    void ToMenu();
    void ShowPanel();
    void HidePanel();
}
