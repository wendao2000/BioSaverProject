﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemies : MonoBehaviour
{
    GameManager gm;

    public ScriptEnemies source; //enemy's information file

    [HideInInspector] public int ID;
    [HideInInspector] public float maxHP, maxMP;

    public float HP, MP;
    public float ATK, DEF;

    public float FLEE;
    public int EXP;

    bool collide = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        name = source.enemiesName;

        ID = source.enemiesID;

        HP = maxHP = source.enemiesHP;
        MP = maxMP = source.enemiesMP;

        ATK = source.enemiesATK;
        DEF = source.enemiesDEF;

        FLEE = source.enemiesFLEE;
        EXP = source.enemiesEXP;

        GetComponent<SpriteRenderer>().sprite = source.enemiesSprite;
        GetComponent<Animator>().runtimeAnimatorController = source.animator;
    }

    void FixedUpdate()
    {
        if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
        {
            if (!gm.interacting)
            {
                StartCoroutine(gm.EnterBattle(ID));
                gm.interacting = true;
            }
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
