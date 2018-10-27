using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterInteraction : MonoBehaviour
{
    public GameManager gm;

    //[HideInInspector]
    public bool interacting = false;
    //[HideInInspector]

    void Start()
    {
        gm = GameManager.GetInstance();
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
