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

    bool collide = false;

    public ShopContent shopContent;

    List<GameObject> shopList = new List<GameObject>();

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            if (!gm.ci.interacting || gm.shop.activeSelf)
            {
                if (!gm.ci.interacting)
                {
                    GenerateShopContent();
                }
                else
                {
                    DestroyShopContent();
                }

                gm.Shop();
            }
        }

        //force close shop using Escape button
        if (gm.shop.activeSelf && CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            gm.Shop();
            DestroyShopContent();
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
        BuyConfirmation();

        while (!gm.bm.pressed)
        {
            yield return null;
        }

        if (gm.bm.confirmed)
        {
            if (gm.currentMoney >= objectToBuy.GetComponent<Inventory>().itemPrice)
            {
                BuyNotification(true);
                gm.currentMoney -= objectToBuy.GetComponent<Inventory>().itemPrice;
                gm.cv.inventoryList.Add(Instantiate(objectToBuy));
                Destroy(gm.cv.inventoryList.LastOrDefault().GetComponent<ShopItem>());
                gm.cv.inventoryList.LastOrDefault().transform.SetParent(gm.inventoryList.transform);
            }
            else
            {
                BuyNotification(false);
            }

            while (!gm.bm.pressed)
            {
                yield return null;
            }
        }

        gm.confirmationPanel.SetActive(false);
    }

    void BuyConfirmation()
    {
        //reset state of button and confirmation text
        gm.bm.pressed = false;
        gm.confirmationPanel.SetActive(true);
        gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Are you sure about this?";
        gm.confirmationPanel.transform.Find("ButtonYes").gameObject.SetActive(true);
        gm.confirmationPanel.transform.Find("ButtonNo").gameObject.SetActive(true);
        gm.confirmationPanel.transform.Find("ButtonConfirm").gameObject.SetActive(false);
    }

    void BuyNotification(bool value)
    {
        //reset state of button again
        gm.bm.pressed = false;
        gm.confirmationPanel.transform.Find("ButtonYes").gameObject.SetActive(false);
        gm.confirmationPanel.transform.Find("ButtonNo").gameObject.SetActive(false);
        gm.confirmationPanel.transform.Find("ButtonConfirm").gameObject.SetActive(true);

        if (value == true)
        {
            gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Purchase Successful!";
        }

        else
        {
            gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Not enough garbage!";
        }
    }

    #region PROTOTYPE -- NEED DATABASE TO CONTINUE
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