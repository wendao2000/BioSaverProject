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

    #region BattleMode
    
    public void Attack()
    {
        Debug.Log("Attack");
    }

    public void Magic()
    {
        Debug.Log("Magic");
        FindObjectOfType<BattleManager>().Magic();
    }

    public void UseItem()
    {
        Debug.Log("Use Item");
    }

    public void Flee()
    {
        Debug.Log("Flee");
        FindObjectOfType<BattleManager>().Flee();
    }

    #endregion

    public void ButtonPress()
    {
        pressed = true;
    }
}
