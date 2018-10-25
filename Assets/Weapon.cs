using UnityEngine;

public class Weapon : MonoBehaviour
{

    public ScriptWeapon source;

    public string itemName;
    public Sprite itemSprite;
    public ItemSlot itemSlot;
    public int minAtk;
    public int maxAtk;
    public int critChance;
    public float critMultiplier;
    public float maxDurability;

    void Start()
    {
        itemName = source.itemName;
        itemSlot = source.itemSlot;
        minAtk = source.minAtk;
        maxAtk = source.maxAtk;
        critChance = source.critChance;
        critMultiplier = source.critMultiplier;
        maxDurability = source.maxDurability;
    }
}