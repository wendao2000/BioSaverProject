using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class ShopManager : MonoBehaviour
{

    Character ch;
    GameManager gm;

    public GameObject shopContentPanel;
    public GameObject confirmationPanel;

    bool collide = false;

    public ShopContent shopContent;

    List<GameObject> shopList = new List<GameObject>();
    int itemCount;

    void Awake()
    {
        ch = FindObjectOfType<Character>();
        gm = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        GenerateShopContent();
    }

    void Update()
    {
        if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            ch.paralyzed = !ch.paralyzed;
            gm.Shop();
        }
    }

    public void GenerateShopContent()
    {
        itemCount = 0;

        if (shopContent.weapon.Length > 0 ||
            shopContent.armor.Length > 0 ||
            shopContent.item.Length > 0)
        {
            for (int i = 0; i < shopContent.weapon.Length; i++)
            {
                shopList.Add(new GameObject("Item " + itemCount.ToString(), typeof(ShopItem)));
                shopList[itemCount].AddComponent<Weapon>().source = shopContent.weapon[i].weapon;
                itemCount++;
            }

            for (int i = 0; i < shopContent.armor.Length; i++)
            {
                shopList.Add(new GameObject("Item " + itemCount.ToString(), typeof(ShopItem)));
                shopList[itemCount].AddComponent<Armor>().source = shopContent.armor[i].armor;
                itemCount++;
            }

            for (int i = 0; i < shopContent.item.Length; i++)
            {
                shopList.Add(new GameObject("Item " + itemCount.ToString(), typeof(ShopItem)));
                shopList[itemCount].AddComponent<Item>().source = shopContent.item[i].item;
                itemCount++;
            }
        }
    }

    public void Buy(string index)
    {
        //reset confirmation panel text
        confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Are you sure about this?";
        confirmationPanel.SetActive(true);
    }

    public void ConfirmYes()
    {
        //confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Successfully bought!";
        //do something before closing the confirmation panel
        confirmationPanel.SetActive(false);
    }

    public void ConfirmNo()
    {
        confirmationPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            //gm.UpdateInteraction("Shop");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = false;
            //gm.UpdateInteraction("Normal");
        }
    }
}
