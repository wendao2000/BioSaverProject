﻿using System.Collections;
using UnityEngine;
using TMPro;

public class EnemiesAI : MonoBehaviour
{
    BattleManager bm;

    public enum EnemyState
    {
        DRAWPHASE,
        MAINPHASE,
        WAITPHASE,
        BATTLEPHASE,
        DEAD
    }

    public EnemyState currentState;

    private float cd, curcd = 0f;

    private bool actionDone = false;

    void Start()
    {
        bm = FindObjectOfType<BattleManager>();

        currentState = EnemyState.DRAWPHASE;
    }

    void Update()
    {
        switch (currentState)
        {
            case (EnemyState.DRAWPHASE):
                DrawPhase();
                break;
            case (EnemyState.MAINPHASE):
                MainPhase();
                break;
            case (EnemyState.WAITPHASE):
                //idle
                break;
            case (EnemyState.BATTLEPHASE):
                StartCoroutine(BattlePhase());
                break;
            case (EnemyState.DEAD):
                Debug.Log(name + " is dead");
                break;
        }
    }

    void DrawPhase()
    {
        if (cd == 0f)
        {
            cd = Random.Range(3f, 5f);
        }

        else if (curcd < cd && (bm.heroQueue.Count == 0 && bm.performList.Count == 0))
        {
            curcd += Time.fixedDeltaTime;
        }

        else if (curcd >= cd)
        {
            cd = curcd = 0f;
            currentState = EnemyState.MAINPHASE;
        }
    }

    void MainPhase()
    {
        TurnHandler attack = new TurnHandler
        {
            source = gameObject,
            target = bm.heroList[Random.Range(0, bm.heroList.Count)]
        };

        bm.SubmitAction(attack);

        currentState = EnemyState.WAITPHASE;
    }

    IEnumerator BattlePhase()
    {
        if (actionDone)
        {
            yield break;
        }

        actionDone = true;

        Vector2 startPos = transform.position;
        Vector2 targetPos = bm.performList[0].target.transform.position;

        while (Move(targetPos))
        {
            yield return null;
        }

        Debug.Log(name + " hit " + bm.performList[0].target.name + " ass");

        yield return new WaitForSeconds(0.5f);

        while (Move(startPos))
        {
            yield return null;
        }

        bm.performList.RemoveAt(0);

        bm.currentState = BattleManager.BattleState.IDLE;

        actionDone = false;

        currentState = EnemyState.DRAWPHASE;
    }

    bool Move(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 20f * Time.deltaTime));
    }
}
