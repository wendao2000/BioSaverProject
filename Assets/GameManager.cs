using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject shop;
    public Button interactionButton;

    public void Shop()
    {
        shop.SetActive(!shop.activeSelf);
    }

    public void UpdateInteraction(string value)
    {
        //update image of interact button
    }

}
