using UnityEngine;

public class Armor : MonoBehaviour
{

    public ScriptArmor source;

    public string itemName;
    public Sprite itemSprite;
    public int armorValue;
    public float maxDurability;

    void Start()
    {
        itemName = source.itemName;
        itemSprite = source.itemSprite;
        armorValue = source.armorValue;
        maxDurability = source.maxDurability;
    }
}