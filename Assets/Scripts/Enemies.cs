using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Enemies : MonoBehaviour {

    bool collide = false;

    public int ID;
    public int HP, MP;
    public float ATK, DEF;
    public float FLEE;
    public int EXP;

    void Update()
    {
        if(collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            FindObjectOfType<GameManager>().EnterBattle(ID);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            //gm.UpdateInteraction("Fight");

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
