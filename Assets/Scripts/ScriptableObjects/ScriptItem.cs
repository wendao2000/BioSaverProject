using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Item")]
public class ScriptItem : ScriptableObject
{
    public int itemID = -1;
    public string itemName = "Item";
    public Sprite itemSprite = null;
    public int itemPrice = 0;
    public ItemSlot itemSlot = ItemSlot.Item;
    public ItemCategory itemCategory;
    public int value;
}

[Serializable]
public class ItemList
{
    public ScriptItem item;
}