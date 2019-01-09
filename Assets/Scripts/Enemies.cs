using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
public class Enemies : MonoBehaviour {

    public ScriptEnemies source;
    
    
    public int ID;
    public int HP, MP;
    public float ATK, DEF;
    public float FLEE;
    public int EXP;

    bool collide = false;

    void Start()
    {
        name = source.enemiesName;
        ID = source.enemiesID;
        HP = source.enemiesHP;
        MP = source.enemiesMP;
        ATK = source.enemiesATK;
        DEF = source.enemiesDEF;
        FLEE = source.enemiesFLEE;
        EXP = source.enemiesEXP;

        GetComponent<SpriteRenderer>().sprite = source.enemiesSprite;
        GetComponent<Animator>().runtimeAnimatorController = source.animator;
    }

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
