﻿using UnityEngine;

public class BattleManager : MonoBehaviour {

    //Equipment eq;
    GameManager gm;

    void Awake()
    {
        //eq = FindObjectOfType<Equipment>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    //float CalculateDamage()
    //{
        //bool crit = Random.Range(0, 100) > critChance ? true : false;
        //float damage = Random.Range(eq.minAtk, eq.maxAtk);
        //damage = crit ? damage * eq.critMultiplier : damage;

        //return damage;
    //}
}
