using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    enum InteractButton
    {
        Normal,
        Shop,
        Battle,
        Exit
    }

    [HideInInspector] public Character chara;

    [HideInInspector] public BattleManager battleManager;
    [HideInInspector] public ButtonManager buttonManager;
    [HideInInspector] public ShopManager shopManager;

    public EnemiesList[] enemies; //temporary database

    [Header("Canvas References")] //redirect to canvas GameObject
    public GameObject charaBriefInfo;
    public GameObject characterInformation;
    public GameObject inventory;
    public GameObject shop;
    public TextMeshProUGUI moneyText;

    [Header("Merchant References")]
    public GameObject shopContentPanel;
    public GameObject confirmationPanel;
    public GameObject inventoryList;

    [Header("Battle References")]
    public Transform mainPanel;
    public Transform secondPanel;

    [Header("Player's Inventory")]
    private readonly int defaultMoney = 1000; //values to be edited
    public int currentMoney = 0;

    [Header("Character's Interaction")]
    public bool interacting = false;

    [Header("General")]
    public Button interactButton; //redirect to virtual interaction button

    [HideInInspector] public Vector2 startPos; //Character startPos based on last exit scene

    //Dictionary<string, Sprite> buttonList = new Dictionary<string, Sprite>(); //doesn't work(?) on Unity

    public Sprite[] buttonList;

    void Start()
    {
        interactButton.GetComponent<Image>().sprite = buttonList[(int)InteractButton.Normal]; //reset virtual interaction button to normal

        chara = FindObjectOfType<Character>();

        battleManager = FindObjectOfType<BattleManager>();
        buttonManager = FindObjectOfType<ButtonManager>();
        shopManager = FindObjectOfType<ShopManager>();

        currentMoney = PlayerPrefs.GetInt("playerMoney", defaultMoney);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Reset();
        }

        moneyText.text = currentMoney.ToString(); //get money

        #region CharacterInteraction
        if (CrossPlatformInputManager.GetButtonDown("Cancel")) //press ESC button or back button on Android
        {
            if (interacting)
            {
                if (characterInformation.activeSelf)
                {
                    CharacterInformation();
                }

                if (inventory.activeSelf)
                {
                    Inventory();
                }

                if (shop.activeSelf)
                {
                    shopManager.DestroyShopContent();
                    Shop();
                }
            }

            else
            {
                Pause();
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("CharacterInformation")) //press C button
        {
            if (!interacting || characterInformation.activeSelf)
            {
                CharacterInformation();
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Inventory")) //press I button
        {
            if (!interacting || inventory.activeSelf)
            {
                Inventory();
            }
        }
        #endregion
    }

    #region UI Interaction
    private void Interact(GameObject input)
    {
        interacting = !interacting;
        chara.charaMove.paralyzed = !chara.charaMove.paralyzed;
        input.SetActive(!input.activeSelf);
    }

    public void CharacterInformation()
    {
        Interact(characterInformation);
    }

    public void Inventory()
    {
        Interact(inventory);
    }

    public void Shop()
    {
        if (shop.activeSelf)
        {
            shopManager.DestroyShopContent();
        }
        else
        {
            shopManager.GenerateShopContent();
        }

        Interact(shop);
    }

    public void UpdateInteraction(string value)
    {
        switch (value)
        {
            case "Normal":
                interactButton.GetComponent<Image>().sprite = buttonList[(int)InteractButton.Normal];
                break;

            case "Shop":
                interactButton.GetComponent<Image>().sprite = buttonList[(int)InteractButton.Shop];
                break;

            case "Battle":
                interactButton.GetComponent<Image>().sprite = buttonList[(int)InteractButton.Battle];
                break;

            case "Exit":
                interactButton.GetComponent<Image>().sprite = buttonList[(int)InteractButton.Exit];
                break;

        }
    }
    #endregion

    #region BattleMode
    public IEnumerator EnterBattle(int index)
    {
        Save();

        PlayerPrefs.SetInt("EnemiesID", index);
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("startPos.X", chara.transform.position.x);
        PlayerPrefs.SetFloat("startPos.Y", chara.transform.position.y);

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("BattleScene");
    }

    public IEnumerator ExitBattle()
    {
        //chara.GainExperience(battleManager.expGained); //broken
        PlayerPrefs.SetInt("pCurEXP", (int)chara.EXP);

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene"));
    }
    #endregion

    #region MainMenu
    public void MainMenu() //play animation
    {
        GameObject.Find("MainMenu").GetComponent<Animator>().SetTrigger("MainMenu");
    }

    public IEnumerator Play() //play animation
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion

    #region Gameplay
    public void Pause()
    {

    }

    public void Setting()
    {

    }
    #endregion

    #region ENEMIES DATABASE???
    public ScriptEnemies GetEnemies(int ID)
    {
        if (ID == 12)
        {
            return enemies[0].enemy;
        }
        else if (ID == 13)
        {
            return enemies[1].enemy;
        }
        else if (ID == 14)
        {
            return enemies[2].enemy;
        }
        else if (ID == 15)
        {
            return enemies[3].enemy;
        }
        else if (ID == 16)
        {
            return enemies[4].enemy;
        }
        else return null;
    }
    #endregion

    #region Miscellaneous
    public void Reset()
    {
        Debug.Log("Reset..");
        chara.LVL = 1;
        chara.EXP = 0;
        PlayerPrefs.DeleteAll();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("pLevel", chara.LVL);
        PlayerPrefs.SetInt("pCurHealth", (int)chara.HP);
        PlayerPrefs.SetInt("pCurMana", (int)chara.MP);
        PlayerPrefs.SetInt("pCurEXP", (int)chara.EXP);
        PlayerPrefs.SetInt("playerMoney", currentMoney);
    }
    #endregion
}