using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopContent
{
    public WeaponList[] weapon;
    public ArmorList[] armor;
    public ItemList[] item;
}

[RequireComponent(typeof(RectTransform), typeof(Image), typeof(Button))]
public class ShopItem : MonoBehaviour
{
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public ItemSlot itemSlot;
    public int itemPrice;

    Navigation nv = new Navigation
    {
        mode = Navigation.Mode.None
    };

    void Awake()
    {
        GetComponent<Button>().navigation = nv;
        GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FindObjectOfType<ShopManager>().Buy(itemID, itemPrice)); });

        transform.SetParent(FindObjectOfType<GameManager>().shopContentPanel.transform);

        GetComponent<RectTransform>().localScale = Vector3.one;
    }
}