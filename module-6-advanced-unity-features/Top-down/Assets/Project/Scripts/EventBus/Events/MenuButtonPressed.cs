public class MenuButtonPressed : IGameEvent
{
    public MenuButtonType ButtonType { get; private set; }

    public MenuButtonPressed(MenuButtonType buttonType)
    {
        ButtonType = buttonType;
    }
}
