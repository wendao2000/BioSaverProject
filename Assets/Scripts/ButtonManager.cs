using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    BattleManager bm;
    GameManager gm;
    StatusManager sm;

    public bool confirmed = false;
    public bool pressed = false;

    void Start()
    {
        bm = FindObjectOfType<BattleManager>();
        gm = GameManager.GetInstance();

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

    #region MainMenu

    public void MainMenu()
    {
        gm.MainMenu();
    }

    public void Play()
    {
        gm.Play();
    }

    #endregion

    public void ButtonPress()
    {
        pressed = true;
    }
}