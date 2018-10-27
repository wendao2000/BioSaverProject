using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class ShopManager : MonoBehaviour
{
    GameManager gm;

    bool collide = false;

    public ShopContent shopContent;

    List<GameObject> shopList = new List<GameObject>();
    int itemCount;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        GenerateShopContent();
    }

    void Update()
    {
        if (gm.shop.activeSelf && CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            gm.Shop();
        }

        if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            if (!gm.ci.interacting || gm.shop.activeSelf)
            {
                gm.Shop();
            }
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
                shopList.Add(new GameObject("Item " + (itemCount + 1).ToString() + ": " + shopContent.weapon[i].weapon.itemName));
                shopList[itemCount].AddComponent<Weapon>().source = shopContent.weapon[i].weapon;
                itemCount++;
            }

            for (int i = 0; i < shopContent.armor.Length; i++)
            {
                shopList.Add(new GameObject("Item " + (itemCount + 1).ToString() + ": " + shopContent.armor[i].armor.itemName));
                shopList[itemCount].AddComponent<Armor>().source = shopContent.armor[i].armor;
                itemCount++;
            }

            for (int i = 0; i < shopContent.item.Length; i++)
            {
                shopList.Add(new GameObject("Item " + (itemCount + 1).ToString() + ": " + shopContent.item[i].item.itemName));
                shopList[itemCount].AddComponent<Item>().source = shopContent.item[i].item;
                itemCount++;
            }
        }
    }

    public IEnumerator Buy(int itemID, int itemPrice)
    {
        FirstState();

        while (!gm.bm.pressed)
        {
            yield return null;
        }

        if (gm.bm.confirmed)
        {
            SecondState();

            if (gm.ci.currentMoney >= itemPrice)
            {
                FinalState(true);
                gm.ci.currentMoney -= itemPrice;
            }
            else
            {
                FinalState(false);
            }

            while (!gm.bm.pressed)
            {
                yield return null;
            }
        }

        gm.confirmationPanel.SetActive(false);
    }

    void FirstState()
    {
        //reset state of button and confirmation text
        gm.bm.pressed = false;
        gm.confirmationPanel.SetActive(true);
        gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Are you sure about this?";
        gm.confirmationPanel.transform.Find("ButtonYes").gameObject.SetActive(true);
        gm.confirmationPanel.transform.Find("ButtonNo").gameObject.SetActive(true);
        gm.confirmationPanel.transform.Find("ButtonConfirm").gameObject.SetActive(false);
    }

    void SecondState()
    {
        //reset state of button again
        gm.bm.pressed = false;
        gm.confirmationPanel.transform.Find("ButtonYes").gameObject.SetActive(false);
        gm.confirmationPanel.transform.Find("ButtonNo").gameObject.SetActive(false);
        gm.confirmationPanel.transform.Find("ButtonConfirm").gameObject.SetActive(true);
    }

    void FinalState(bool value)
    {
        if (value == true)
        {
            gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Purchase Successful!";
        }

        else
        {
            gm.confirmationPanel.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Not enough garbage!";
        }
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
