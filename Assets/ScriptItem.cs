using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Item")]
public class ScriptItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite = null;
    public ItemCategory itemCategory;
    public int value;
}

[Serializable]
public class ItemList
{
    public ScriptItem item;
}