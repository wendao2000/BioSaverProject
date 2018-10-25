using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class ScriptArmor : ScriptableObject
{
    public string itemName = "Armor";
    public Sprite itemSprite = null;
    public ItemSlot itemSlot = ItemSlot.Armor;
    public int armorValue = 0;
    public float maxDurability = 0;
}

[Serializable]
public class ArmorList
{
    public ScriptArmor armor;
}