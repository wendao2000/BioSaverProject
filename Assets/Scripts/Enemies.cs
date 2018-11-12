using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Enemies : MonoBehaviour {

    bool collide = false;

    void Update()
    {
        if(collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            FindObjectOfType<GameManager>().EnterBattle(new int[] {0});
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            //gm.UpdateInteraction("Shop");

            Debug.Log("Enter Enemies");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = false;
            //gm.UpdateInteraction("Normal");

            Debug.Log("Exit Enemies");
        }
    }
}
