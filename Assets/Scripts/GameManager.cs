using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public ButtonManager bm;
    [HideInInspector] public CharacterMovement ch;
    [HideInInspector] public CharacterInteraction ci;
    [HideInInspector] public CharacterInventory cv;

    [Header("Canvas References")]
    //redirect to canvas
    public GameObject shop;
    public GameObject inventory;
    public TextMeshProUGUI moneyText;

    [Header("Merchant References")]
    //merchant needs this
    public GameObject shopContentPanel;
    public GameObject confirmationPanel;
    //item goes here
    public GameObject inventoryList;

    //Character Start Position based on last exit scene
    //[HideInInspector]
    public Vector2 startPos;

    [Header("Player's Inventory")]
    public int defaultMoney = 500;
    public int currentMoney = 0;

    [Header("General")]
    //redirect to virtual interaction button
    public Button interactionButton;

    void Awake()
    {
        bm = FindObjectOfType<ButtonManager>();
        ch = FindObjectOfType<CharacterMovement>();
        ci = FindObjectOfType<CharacterInteraction>();
        cv = FindObjectOfType<CharacterInventory>();
        
        startPos = new Vector2(
            PlayerPrefs.HasKey("startPos.X") ? PlayerPrefs.GetFloat("startPos.X", -8f) : -8f,
            PlayerPrefs.HasKey("startPos.Y") ? PlayerPrefs.GetFloat("startPos.Y", 3f) : 3f
            );
        ch.transform.position = startPos;
        currentMoney = PlayerPrefs.HasKey("playerMoney") ? PlayerPrefs.GetInt("playerMoney", defaultMoney) : defaultMoney;
    }

    void Update()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void Shop()
    {
        ch.paralyzed = !ch.paralyzed;
        ci.interacting = !ci.interacting;
        shop.SetActive(!shop.activeSelf);
    }

    public void Inventory()
    {
        ch.paralyzed = !ch.paralyzed;
        ci.interacting = !ci.interacting;
        inventory.SetActive(!inventory.activeSelf);
    }

    public void UpdateInteraction(string value)
    {
        //update image of interact button
    }

    public void Save()
    {
        PlayerPrefs.SetInt("playerMoney", currentMoney);
    }
}
