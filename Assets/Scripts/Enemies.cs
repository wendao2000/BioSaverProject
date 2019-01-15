using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemies : MonoBehaviour
{
    GameManager gm;

    public ScriptEnemies source; //enemy's information file

    [HideInInspector] public int ID;
    [HideInInspector] public float maxHP, maxMP;

    [HideInInspector] public float HP, MP;
    [HideInInspector] public float ATK, DEF;

    [HideInInspector] public float FLEE;
    [HideInInspector] public int EXP;

    bool collide = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        name = source.enemiesName;

        ID = source.enemiesID;

        maxHP = HP = source.enemiesHP;
        maxMP = MP = source.enemiesMP;

        ATK = source.enemiesATK;
        DEF = source.enemiesDEF;

        FLEE = source.enemiesFLEE;
        EXP = source.enemiesEXP;

        GetComponent<SpriteRenderer>().sprite = source.enemiesSprite;
        GetComponent<Animator>().runtimeAnimatorController = source.animator;
    }

    void Update()
    {
        if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            StartCoroutine(gm.EnterBattle(ID));
        }
    }

    private void OnMouseDown() //BattleMode
    {
        if (gm.battleManager != null)
        {
            gm.battleManager.statusManager.GetObject(null, this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            gm.UpdateInteraction("Battle");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = false;
            gm.UpdateInteraction("Normal");
        }
    }
}
