using UnityEngine;
public class PlayerInputReader : MonoBehaviour
{
    public bool FireButtonPressed { get; private set; }

    private void Update()
    {
        HandleButtons();

    }

    private void HandleButtons()
    {
        FireButtonPressed = Input.GetButton("Fire1");
    }
}
