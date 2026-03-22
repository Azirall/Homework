using System;

public interface IMainMenuView
{
    event Action StartRequested;

    void Start();
    void Exit();
    void ShowPanel();
    void HidePanel();
}
