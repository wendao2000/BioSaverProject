using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public static ButtonManager instance;

    public bool confirmed = false;
    public bool pressed = false;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
