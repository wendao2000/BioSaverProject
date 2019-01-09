using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAI : MonoBehaviour
{
    BattleManager bm;
    CharacterStatus cs;

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
    public Image cooldownBar;

    private bool actionDone = false;

    void Start()
    {
        bm = FindObjectOfType<BattleManager>();
        cs = GetComponent<CharacterStatus>();

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
        float cooldownRatio;

        if (cd == 0f)
        {
            cd = Random.Range(2f, 2.5f);
        }

        else if (curcd < cd && (bm.heroQueue.Count == 0 && bm.performList.Count == 0))
        {
            curcd += Time.fixedDeltaTime;
            cooldownRatio = curcd / cd;
            cooldownBar.transform.localScale = new Vector3(Mathf.Clamp(cooldownRatio, 0, 1), cooldownBar.transform.localScale.y, cooldownBar.transform.localScale.z);
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

        Attack(bm.performList[0].target.GetComponent<Enemies>());

        yield return new WaitForSeconds(0.2f);

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

    private void Attack(Enemies enemy)
    {
        bool crit = (cs.CRIT > Random.Range(0, 100)) ? true : false;
        float attack = Random.Range(cs.minAtk, cs.maxAtk);
        attack = Mathf.FloorToInt(crit ? attack * cs.CRIT_MULTIPLIER : attack);

        int damageGiven = Mathf.FloorToInt((attack * attack) / (attack + enemy.DEF));
        enemy.HP = Mathf.Clamp(enemy.HP - damageGiven, 0, enemy.HP);

        Debug.Log("damage: " + damageGiven);
        Debug.Log("enemy's HP: " + enemy.HP);
    }
}
