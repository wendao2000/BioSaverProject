using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAI : MonoBehaviour
{
    Animator an;
    BattleManager bm;
    Character chara;

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
        an = GetComponent<Animator>();
        bm = FindObjectOfType<BattleManager>();
        chara = GetComponent<Character>();

        GameObject.Find("Selector").SetActive(false);

        currentState = HeroState.DRAWPHASE;
    }

    void Update()
    {
        if (chara.HP == 0)
        {
            //an.SetTrigger("Dead"); //no animation, yet.
            bm.heroDead++;
            currentState = HeroState.DEAD;
            chara.HP = -1;
        }

        switch (currentState)
        {
            case (HeroState.DRAWPHASE):
                DrawPhase();
                break;
            case (HeroState.MAINPHASE):
                //wait for input
                break;
            case (HeroState.WAITPHASE):
                //idle
                break;
            case (HeroState.BATTLEPHASE):
                StartCoroutine(BattlePhase());
                break;
            case (HeroState.DEAD):
                //die
                break;
        }
    }

    private void DrawPhase()
    {
        float cooldownRatio;

        if (bm.currentState != BattleManager.BattleState.ENDBATTLE)
        {
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
    }

    private IEnumerator BattlePhase()
    {
        if (actionDone)
        {
            yield break;
        }

        actionDone = true;

        Vector2 startPos = transform.position;
        Vector2 targetPos = bm.performList[0].target.transform.position;

        an.SetTrigger("Attack");

        GetComponent<SpriteRenderer>().sortingOrder = 1;

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

        GetComponent<SpriteRenderer>().sortingOrder = 0;

        bm.performList.RemoveAt(0);

        bm.currentState = BattleManager.BattleState.IDLE;

        actionDone = false;

        currentState = HeroState.DRAWPHASE;
    }

    private bool Move(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 20f * Time.deltaTime));
    }

    private void Attack(Enemies enemy)
    {
        bool crit = (chara.CRIT > Random.Range(0, 100)) ? true : false;
        float attack = Random.Range(chara.minAtk, chara.maxAtk);
        attack = Mathf.FloorToInt(crit ? attack * chara.CRIT_MULTIPLIER : attack);

        int damageGiven = Mathf.FloorToInt((attack * attack) / (attack + enemy.DEF));
        enemy.HP = Mathf.Clamp(enemy.HP - damageGiven, 0, enemy.HP);
    }
}