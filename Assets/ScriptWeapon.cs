using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class ScriptWeapon : ScriptableObject
{
    
    public string itemName = "Weapon";
    public Sprite itemSprite = null;
    public ItemSlot itemSlot = ItemSlot.Weapon;
    public int minAtk = 0;
    public int maxAtk = 0;
    public int critChance = 0;
    public float critMultiplier = 0;
    public float maxDurability = 0;
}

[Serializable]
public class WeaponList
{
    public ScriptWeapon weapon;
}