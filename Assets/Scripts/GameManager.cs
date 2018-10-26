using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //redirect to canvas
    public GameObject shop;
    public GameObject inventory;

    //redirect to virtual interaction button
    public Button interactionButton;

    public void Shop()
    {
        shop.SetActive(!shop.activeSelf);
    }

    public void Inventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    public void UpdateInteraction(string value)
    {
        //update image of interact button
    }

}
