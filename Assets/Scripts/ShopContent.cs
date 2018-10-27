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

public class ShopItem : MonoBehaviour
{
    Navigation nv = new Navigation
    {
        mode = Navigation.Mode.None
    };

    void Start()
    {
        GetComponent<Button>().navigation = nv;
        GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FindObjectOfType<ShopManager>().Buy(gameObject)); } );
    }
}