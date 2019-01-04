using System.Collections;
using UnityEngine;
using TMPro;

public class CharacterAI : MonoBehaviour
{
    BattleManager bm;

    public enum HeroState
    {
        DRAWPHASE,
        MAINPHASE,
        WAITPHASE,
        BATTLEPHASE,
        DEAD
    }

    public HeroState currentState;

    private float cd, curcd = 0f;

    private bool actionDone = false;

    void Start()
    {
        bm = FindObjectOfType<BattleManager>();
        
        GameObject.Find("Selector").SetActive(false);

        currentState = HeroState.DRAWPHASE;
    }

    void Update()
    {
        switch (currentState)
        {
            case (HeroState.DRAWPHASE):
                DrawPhase();
                break;
            case (HeroState.MAINPHASE):
                //wait for input
                break;
            case (HeroState.WAITPHASE):
                //idle | wait for turn
                break;
            case (HeroState.BATTLEPHASE):
                StartCoroutine(BattlePhase());
                break;
            case (HeroState.DEAD):
                Debug.Log(name + " is dead");
                break;
        }
    }

    void DrawPhase()
    {
        if (cd == 0f)
        {
            cd = Random.Range(2f, 3f);
        }

        else if (curcd < cd && (bm.heroQueue.Count == 0 && bm.performList.Count == 0))
        {
            curcd += Time.fixedDeltaTime;
        }

        else if (curcd >= cd)
        {
            cd = curcd = 0f;
            bm.heroQueue.Add(gameObject);
            currentState = HeroState.MAINPHASE;
        }
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

        currentState = HeroState.DRAWPHASE;
    }

    bool Move(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 20f * Time.deltaTime));
    }
}
