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

    public static GameManager instance;

    [HideInInspector] public BattleManager battleManager;
    [HideInInspector] public ButtonManager buttonManager;
    [HideInInspector] public CharacterMovement charaMove;
    [HideInInspector] public CharacterInventory charaInven;
    [HideInInspector] public ShopManager shopManager;

    public EnemiesList[] enemies; //temporary database

    [Header("Canvas References")] //redirect to canvas GameObject
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

        battleManager = FindObjectOfType<BattleManager>();
        buttonManager = FindObjectOfType<ButtonManager>();
        charaMove = FindObjectOfType<CharacterMovement>();
        charaInven = FindObjectOfType<CharacterInventory>();
        shopManager = FindObjectOfType<ShopManager>();
    }

    void Update()
    {
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
    public void CharacterInformation()
    {
        interacting = !interacting;
        charaMove.paralyzed = !charaMove.paralyzed;
        characterInformation.SetActive(!characterInformation.activeSelf);
    }

    public void Inventory()
    {
        charaMove.paralyzed = !charaMove.paralyzed;
        interacting = !interacting;
        inventory.SetActive(!inventory.activeSelf);
    }

    public void Shop()
    {
        if (!shop.activeSelf)
        {
            shopManager.GenerateShopContent();
        }

        else
        {
            shopManager.DestroyShopContent();
        }

        charaMove.paralyzed = !charaMove.paralyzed;
        interacting = !interacting;
        shop.SetActive(!shop.activeSelf);
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
        PlayerPrefs.SetInt("EnemiesID", index);
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("startPos.X", charaMove.transform.position.x);
        PlayerPrefs.SetFloat("startPos.Y", charaMove.transform.position.y);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BattleScene");
    }

    public IEnumerator ExitBattle()
    {
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
        else return null;
    }
    #endregion

    #region Miscellaneous
    public void Save()
    {
        PlayerPrefs.SetInt("playerMoney", currentMoney);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //startPos = new Vector2(PlayerPrefs.GetFloat("startPos.X"),PlayerPrefs.GetFloat("startPos.Y"));

        currentMoney = PlayerPrefs.GetInt("playerMoney", defaultMoney);
    }
    #endregion
}