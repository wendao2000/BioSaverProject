using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class ShopManager : MonoBehaviour
{
    GameManager gm;

    Transform confirmPanel;

    bool collide = false;

    public ShopContent shopContent;

    List<GameObject> shopList = new List<GameObject>();

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        confirmPanel = gm.confirmationPanel.transform;
    }

    void FixedUpdate()
    {
        if (CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            if (collide || gm.shop.activeSelf)
            {
                if (gm.confirmationPanel.activeSelf)
                {
                    gm.confirmationPanel.SetActive(false);
                }

                gm.Shop();
            }
        }
    }

    public void GenerateShopContent()
    {
        if (shopContent.weapon.Length > 0 ||
            shopContent.armor.Length > 0 ||
            shopContent.item.Length > 0)
        {
            for (int i = 0; i < shopContent.weapon.Length; i++)
            {
                shopList.Add(new GameObject(shopContent.weapon[i].weapon.itemName, typeof(ShopItem)));
                shopList.LastOrDefault().AddComponent<Weapon>().source = shopContent.weapon[i].weapon;
            }

            for (int i = 0; i < shopContent.armor.Length; i++)
            {
                shopList.Add(new GameObject(shopContent.armor[i].armor.itemName, typeof(ShopItem)));
                shopList.LastOrDefault().AddComponent<Armor>().source = shopContent.armor[i].armor;
            }

            for (int i = 0; i < shopContent.item.Length; i++)
            {
                shopList.Add(new GameObject(shopContent.item[i].item.itemName, typeof(ShopItem)));
                shopList.LastOrDefault().AddComponent<Item>().source = shopContent.item[i].item;
            }
        }
    }

    public void DestroyShopContent()
    {
        for (int i = 0; i < gm.shopContentPanel.transform.childCount; i++)
        {
            Destroy(gm.shopContentPanel.transform.GetChild(i).gameObject);
        }
        shopList.Clear();
    }

    public IEnumerator Buy(GameObject objectToBuy)
    {
        int itemPrice = objectToBuy.GetComponent<Inventory>().itemPrice;

        BuyConfirmation(itemPrice);

        while (!gm.buttonManager.pressed)
        {
            yield return null;
        }

        if (gm.buttonManager.confirmed)
        {
            if (gm.currentMoney >= itemPrice)
            {
                BuyNotification(true);
                gm.currentMoney -= itemPrice;
                gm.chara.charaInven.inventoryList.Add(Instantiate(objectToBuy));
                Destroy(gm.chara.charaInven.inventoryList.LastOrDefault().GetComponent<ShopItem>());
                gm.chara.charaInven.inventoryList.LastOrDefault().transform.SetParent(gm.inventoryList.transform);
            }
            else
            {
                BuyNotification(false);
            }

            while (!gm.buttonManager.pressed)
            {
                yield return null;
            }
        }

        gm.confirmationPanel.SetActive(false);
    }

    void BuyConfirmation(int itemPrice)
    {
        //reset state of button and confirmation text
        gm.buttonManager.pressed = false;
        gm.confirmationPanel.SetActive(true);
        confirmPanel.Find("Description").GetComponent<TextMeshProUGUI>().text = "Price: " + itemPrice + "\nAre you sure about this?";
        confirmPanel.Find("ButtonYes").gameObject.SetActive(true);
        confirmPanel.Find("ButtonNo").gameObject.SetActive(true);
        confirmPanel.Find("ButtonConfirm").gameObject.SetActive(false);
    }

    void BuyNotification(bool value)
    {
        //reset state of button again
        gm.buttonManager.pressed = false;
        confirmPanel.Find("ButtonYes").gameObject.SetActive(false);
        confirmPanel.Find("ButtonNo").gameObject.SetActive(false);
        confirmPanel.Find("ButtonConfirm").gameObject.SetActive(true);

        if (value == true)
        {
            confirmPanel.Find("Description").GetComponent<TextMeshProUGUI>().text = "Purchase Successful!";
        }

        else
        {
            confirmPanel.Find("Description").GetComponent<TextMeshProUGUI>().text = "Not enough garbage!";
        }
    }

    #region ITEM DATABASE
    Type ItemSlotCheck(int itemID)
    {
        //check itemID from database
        if (itemID == 10)
        {
            return typeof(Weapon);
        }
        else if (itemID == 20)
        {
            return typeof(Armor);
        }
        else if (itemID == 30)
        {
            return typeof(Item);
        }
        else return null;
    }
    #endregion

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            gm.UpdateInteraction("Shop");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = false;
            gm.UpdateInteraction("Normal");
        }
    }
}