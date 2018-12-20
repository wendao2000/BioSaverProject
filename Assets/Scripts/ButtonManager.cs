using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public static ButtonManager instance;

    GameManager gm;
    BattleManager bm;

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

    void Start()
    {
        gm = GameManager.GetInstance();
        bm = FindObjectOfType<BattleManager>();
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
        if (gm.magicPanel.activeSelf || gm.itemPanel.activeSelf)
        {
            if (gm.magicPanel.activeSelf)
            {
                gm.magicPanel.SetActive(false);
            }
            else if (gm.itemPanel.activeSelf)
            {
                gm.itemPanel.SetActive(false);
            }
        }
        
        bm.Attack();
    }

    public void Magic()
    {
        FindObjectOfType<BattleManager>().Magic();
    }

    public void UseItem()
    {
        FindObjectOfType<BattleManager>().UseItem();
    }

    public void Flee()
    {
        FindObjectOfType<BattleManager>().Flee();
    }

    #endregion

    public void ButtonPress()
    {
        pressed = true;
    }
}
