using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterInteraction : MonoBehaviour
{
    GameManager gm;

    //[HideInInspector]
    public bool interacting = false;
    //[HideInInspector]
    public int currentMoney = 0;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gm.inventory.activeSelf && CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            gm.Inventory();
        }

        if (CrossPlatformInputManager.GetButtonDown("Inventory"))
        {
            if (!interacting || gm.inventory.activeSelf)
            {
                gm.Inventory();
            }
        }
    }
}
