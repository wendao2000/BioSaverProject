using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public BattleManager bm;
    [HideInInspector] public ButtonManager bt;
    [HideInInspector] public CharacterMovement ch;
    [HideInInspector] public CharacterInventory cv;
    [HideInInspector] public ShopManager sm;

    [Header("Canvas References")]
    //redirect to canvas
    public GameObject shop;
    public GameObject inventory;
    public GameObject characterInformation;
    public GameObject battleMode;
    public TextMeshProUGUI moneyText;

    [Header("Merchant References")]
    //merchant needs this
    public GameObject shopContentPanel;
    public GameObject confirmationPanel;
    //item goes here
    public GameObject inventoryList;

    [Header("Battle References")]
    public GameObject mainPanel;
    public GameObject magicPanel;
    public GameObject itemPanel;

    [Header("Player's Inventory")]
    public int defaultMoney = 500;
    public int currentMoney = 0;

    [Header("Character's Interaction")]
    public bool interacting = false;

    [Header("General")]
    public bool inBattle = false;
    public Button interactionButton; //redirect to virtual interaction button

    //Character Start Position based on last exit scene
    [HideInInspector] public Vector2 startPos;

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

    void Update()
    {
        moneyText.text = currentMoney.ToString();

        #region Character Interaction
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            if (interacting)
            {
                if (shop.activeSelf)
                {
                    sm.DestroyShopContent();
                    Shop();
                }

                if (characterInformation.activeSelf)
                {
                    CharacterInformation();
                }

                if (inventory.activeSelf)
                {
                    Inventory();
                }

                //if (pauseMenu.activeSelf)
                //{
                //    PauseMenu();
                //}
            }

            else
            {
                //PauseMenu();
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("CharacterInformation"))
        {
            if (!interacting || characterInformation.activeSelf)
            {
                CharacterInformation();
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Inventory"))
        {
            if (!interacting || inventory.activeSelf)
            {
                Inventory();
            }
        }
        #endregion
    }

    #region UI Interaction

    public void Shop()
    {
        if (!shop.activeSelf)
        {
            sm.GenerateShopContent();
        }
        else
        {
            sm.DestroyShopContent();
        }

        ch.paralyzed = !ch.paralyzed;
        interacting = !interacting;
        shop.SetActive(!shop.activeSelf);
    }

    public void CharacterInformation()
    {
        ch.paralyzed = !ch.paralyzed;
        interacting = !interacting;
        characterInformation.SetActive(!characterInformation.activeSelf);
    }

    public void Inventory()
    {
        ch.paralyzed = !ch.paralyzed;
        interacting = !interacting;
        inventory.SetActive(!inventory.activeSelf);
    }

    public void UpdateInteraction(string value)
    {
        //update image of interact button
    }

    #endregion

    public void EnterBattle(int index)
    {
        PlayerPrefs.SetInt("EnemiesID", index);
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("startPos.X", ch.transform.position.x);
        PlayerPrefs.SetFloat("startPos.Y", ch.transform.position.y);
        battleMode.SetActive(true);
        SceneManager.LoadScene("battleScene");
    }

    public void ExitBattle()
    {
        battleMode.SetActive(false);
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene"));
    }

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
        bt = FindObjectOfType<ButtonManager>();
        bm = FindObjectOfType<BattleManager>();
        ch = FindObjectOfType<CharacterMovement>();
        cv = FindObjectOfType<CharacterInventory>();
        sm = FindObjectOfType<ShopManager>();

        startPos = new Vector2(
            PlayerPrefs.HasKey("startPos.X") ? PlayerPrefs.GetFloat("startPos.X", -8f) : -8f,
            PlayerPrefs.HasKey("startPos.Y") ? PlayerPrefs.GetFloat("startPos.Y", -3f) : -3f
            );

        if (!inBattle)
        {
            ch.transform.position = startPos;
        }
        currentMoney = PlayerPrefs.HasKey("playerMoney") ? PlayerPrefs.GetInt("playerMoney", defaultMoney) : defaultMoney;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
