﻿using UnityEngine.UI;

public class Armor : Inventory
{
    public ScriptArmor source;

    public int armorValue;
    public float maxDurability;

    void Start()
    {
        itemID = source.itemID;
        name = itemName = source.itemName;
        itemSprite = source.itemSprite;
        itemSlot = source.itemSlot;
        itemPrice = source.itemPrice;
        armorValue = source.armorValue;
        maxDurability = source.maxDurability;

        GetComponent<Image>().sprite = itemSprite;
    }
}