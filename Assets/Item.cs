using UnityEngine;

public class Item : MonoBehaviour
{

    public ScriptItem source;

    public string itemName;
    public Sprite itemSprite;
    public ItemCategory itemCategory;
    public int value;

    void Start()
    {
        itemName = source.itemName;
        itemSprite = source.itemSprite;
        itemCategory = source.itemCategory;
        value = source.value;
    }
}
