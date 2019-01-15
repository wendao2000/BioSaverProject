using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    GameManager gm;

    public bool confirmed = false;
    public bool pressed = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    #region BattleMode
    public void BattleAttack()
    {
        gm.battleManager.Attack();
    }

    public void BattleMagic()
    {
        gm.battleManager.Magic();
    }

    public void BattleItem()
    {
        gm.battleManager.UseItem();
    }

    public void BattleFlee()
    {
        gm.battleManager.Flee();
    }

    public void ClosePanel()
    {
        gm.battleManager.statusManager.Toggle();
    }

    public void ContinueJourney()
    {
        StartCoroutine(gm.ExitBattle());
    }
    #endregion

    #region MainMenu
    public void MainMenu()
    {
        gm.MainMenu();
    }

    public void Play()
    {
        StartCoroutine(gm.Play());
    }
    #endregion

    #region Gameplay
    public void Setting()
    {
        gm.Setting();
    }
    #endregion

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