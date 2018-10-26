﻿using UnityEngine;

public class Item : ShopItem
{

    public ScriptItem source;
    
    public ItemCategory itemCategory;
    public int value;

    void Start()
    {
        itemID = source.itemID;
        itemName = source.itemName;
        itemSprite = source.itemSprite;
        itemSlot = source.itemSlot;
        itemPrice = source.itemPrice;
        itemCategory = source.itemCategory;
        value = source.value;
    }
}