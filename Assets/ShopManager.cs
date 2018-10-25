using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShopManager : MonoBehaviour {

    Character ch;
    GameManager gm;

    bool collide = false;

    void Awake()
    {
        ch = FindObjectOfType<Character>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update () {
		if(collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            gm.Shop();
            ch.paralyzed = !ch.paralyzed;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collide = true;
            //gm.UpdateInteraction("Shop");
        }
    }
}
