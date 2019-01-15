using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public ItemSlot itemSlot;
    public int itemPrice;

    void Awake()
    {
        if (!GetComponent<RectTransform>())
        {
            gameObject.AddComponent<RectTransform>();
            gameObject.AddComponent<Image>();
            gameObject.AddComponent<Button>();
        }

        transform.SetParent(FindObjectOfType<GameManager>().shopContentPanel.transform);

        GetComponent<RectTransform>().localScale = Vector3.one;
    }
}
