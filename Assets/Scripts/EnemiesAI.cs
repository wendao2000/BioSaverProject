using System.Collections;
using UnityEngine;

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
    
    private float cd;
    private float curcd = 0f;

    //for animation
    private Vector2 startPos;
    private Vector2 heroPos;

    private bool actionDone = false;

    void Start()
    {
        bm = FindObjectOfType<BattleManager>();
        startPos = transform.position;
    }

    void Update()
    {
        switch(currentState)
        {
            case (EnemyState.DRAWPHASE):
                DrawPhase();
                break;
            case (EnemyState.MAINPHASE):
                MainPhase();
                break;
            case (EnemyState.WAITPHASE):

                break;
            case (EnemyState.BATTLEPHASE):
                StartCoroutine(BattlePhase());
                break;
            case (EnemyState.DEAD):

                Debug.Log("Enemies Defeated!");
                break;
        }
    }

    void DrawPhase()
    {
        if (cd == 0f)
        {
            cd = Random.Range(4f, 5f);
        }

        if (curcd < cd)
        {
            curcd += Time.deltaTime;
        }
        else
        {
            cd = 0f;
            currentState++;
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

        currentState++;
    }

    private IEnumerator BattlePhase()
    {
        if (actionDone)
        {
            yield break;
        }
        
        actionDone = true;

        //do animation of enemy attacking player.

        Debug.Log(name + " hit yo ass");

        bm.performList.RemoveAt(0);

        bm.currentState = BattleManager.BattleState.IDLE;

        actionDone = false;

        curcd = 0f;
        currentState = EnemyState.DRAWPHASE;
    }
}
