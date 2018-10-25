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
    Navigation nv = new Navigation
    {
        mode = Navigation.Mode.None
    };

    void Awake()
    {
        GetComponent<Button>().navigation = nv;
        GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<ShopManager>().Buy(name); });
        transform.SetParent(FindObjectOfType<ShopManager>().shopContentPanel.transform);
        GetComponent<RectTransform>().localScale = Vector3.one;
    }
}