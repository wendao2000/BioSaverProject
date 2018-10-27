using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image), typeof(Button))]
public class Inventory : MonoBehaviour {

    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public ItemSlot itemSlot;
    public int itemPrice;

    private void Awake()
    {
        transform.SetParent(FindObjectOfType<GameManager>().shopContentPanel.transform);

        GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
