using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Enemies : MonoBehaviour {

    bool collide = false;

    public int enemyID;

    void Update()
    {
        if(collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            FindObjectOfType<GameManager>().EnterBattle(enemyID);
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
