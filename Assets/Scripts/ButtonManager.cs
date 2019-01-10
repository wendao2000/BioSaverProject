using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public static ButtonManager instance;

    BattleManager bm;
    StatusManager sm;

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
        bm = FindObjectOfType<BattleManager>();

        if (bm != null)
        {
            sm = bm.GetComponent<StatusManager>();
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
        bm.Attack();
    }

    public void Magic()
    {
        bm.Magic();
    }

    public void UseItem()
    {
        bm.UseItem();
    }

    public void Flee()
    {
        bm.Flee();
    }

    public void ToggleStatusPanel()
    {
        sm.statusPanel.SetActive(!sm.statusPanel.activeSelf);
        sm.toggleButton.SetActive(!sm.toggleButton.activeSelf);
    }

    #endregion

    public void ButtonPress()
    {
        pressed = true;
    }
}