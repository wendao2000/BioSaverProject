using UnityEngine;

public class Weapon : ShopItem
{

    public ScriptWeapon source;
    
    public int minAtk;
    public int maxAtk;
    public int critChance;
    public float critMultiplier;
    public float maxDurability;

    void Start()
    {
        itemID = source.itemID;
        itemName = source.itemName;
        itemSprite = source.itemSprite;
        itemSlot = source.itemSlot;
        itemPrice = source.itemPrice;
        minAtk = source.minAtk;
        maxAtk = source.maxAtk;
        critChance = source.critChance;
        critMultiplier = source.critMultiplier;
        maxDurability = source.maxDurability;
    }
}