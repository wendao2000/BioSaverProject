using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemSlot
{
    Weapon,
    Armor,
    Item
};

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Super_Rare,
    Ultra_Rare
};

public enum ItemCategory
{
    Potion
};

public class CharacterInventory : MonoBehaviour
{
    public List<GameObject> inventoryList = new List<GameObject>();
}