using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public bool confirmed = false;
    public bool pressed = false;

    public void Confirm(bool value)
    {
        if (value == true)
        {
            confirmed = true;
        }

        else
        {
            confirmed = false;
        }
    }

    public void ButtonPress()
    {
        pressed = true;
    }
}
