using UnityEngine;
using UnityEngine.UI;

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


    [Header("Merchant-Canvas References")]
    //merchant needs this
    public GameObject shopContentPanel;
    public GameObject confirmationPanel;

    //Character Start Position based on last exit scene
    //[HideInInspector]
    public Vector2 startPos;

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
            PlayerPrefs.HasKey("startPos.Y") ? PlayerPrefs.GetFloat("startPos.Y", 3f) : 3f);
        GameObject.Find("Character").transform.position = startPos;
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
}
