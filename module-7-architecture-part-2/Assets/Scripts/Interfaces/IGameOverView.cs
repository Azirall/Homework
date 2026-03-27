using System;

public interface IGameOverView
{
    event Action RestartRequested;

    void RestartToMenu();
    void ShowPanel();
    void HidePanel();
}
