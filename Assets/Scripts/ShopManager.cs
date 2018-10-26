using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    CharacterMovement ch;
    CharacterInteraction ci;
    GameManager gm;
    CharacterInventory cv;

    public GameObject shopContentPanel;
    public GameObject confirmationPanel;

    bool collide = false;
    bool buyConfirm = false;

    public ShopContent shopContent;

    List<GameObject> shopList = new List<GameObject>();
    int itemCount;

    void Awake()
    {
        ch = FindObjectOfType<CharacterMovement>();
        ci = FindObjectOfType<CharacterInteraction>();
        gm = FindObjectOfType<GameManager>();
        cv = FindObjectOfType<CharacterInventory>();
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
                shopList.Add(new GameObject("Item " + itemCount.ToString()));
                shopList[itemCount].AddComponent<Weapon>().source = shopContent.weapon[i].weapon;
                itemCount++;
            }

            for (int i = 0; i < shopContent.armor.Length; i++)
            {
                shopList.Add(new GameObject("Item " + itemCount.ToString()));
                shopList[itemCount].AddComponent<Armor>().source = shopContent.armor[i].armor;
                itemCount++;
            }

            for (int i = 0; i < shopContent.item.Length; i++)
            {
                shopList.Add(new GameObject("Item " + itemCount.ToString()));
                shopList[itemCount].AddComponent<Item>().source = shopContent.item[i].item;
                itemCount++;
            }
        }
    }

    public IEnumerator Buy(int itemID, int itemPrice)
    {
        //reset confirmation text
        confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Are you sure about this?";
        confirmationPanel.SetActive(true);

        while (confirmationPanel.activeSelf)
        {
            yield return null;
        }

        if (buyConfirm)
        {
            //if got enough money
            if (ci.currentMoney >= itemPrice)
            {
                ci.currentMoney -= itemPrice;
                ci.
            }
        }
    }

    public void Confirm(bool value)
    {
        if (value == true)
        {
            buyConfirm = true;
        }

        else buyConfirm = false;
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
