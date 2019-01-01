using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{

    //CharacterStatus cs;
    //GameManager gm;

    public enum BattleState
    {
        IDLE,
        TAKEACTION,
        PERFORMACTION
    }

    public BattleState currentState;

    public List<TurnHandler> performList = new List<TurnHandler>();
    public List<GameObject> heroList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();

    void Start()
    {
        currentState = BattleState.IDLE;
        heroList.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        switch (currentState)
        {
            case (BattleState.IDLE):
                if (performList.Count > 0)
                {
                    currentState++;
                }
                break;
            case (BattleState.TAKEACTION):
                TakeAction();
                break;
            case (BattleState.PERFORMACTION):

                break;
        }
    }

    private void GenerateEnemies(int index)
    {

    }

    public void Attack()
    {
        //int playerAtk = CalculateDamage();
        //int damageGiven = Mathf.FloorToInt((playerAtk * playerAtk) / (playerAtk + enemies.DEF));
        //GameObject.Find("DamageGiven").GetComponent<TextMeshProUGUI>().text = "Damage Given: " + Mathf.FloorToInt(damageGiven);
        //enemies.HP = Mathf.Clamp(enemies.HP - damageGiven, 0, enemies.HP);
    }

    public void Magic()
    {
        //if (gm.itemPanel.activeSelf)
        //     {
        //         gm.itemPanel.SetActive(false);
        //     }
        //
        // gm.magicPanel.SetActive(true);
    }

    public void UseItem()
    {
        //if (gm.magicPanel.activeSelf)
        //     {
        //         gm.magicPanel.SetActive(false);
        //     }
        //
        //gm.itemPanel.SetActive(true);
    }

    public void Flee()
    {
        //using random value, if satisfy, successfully escaped, vice versa
        // gm.ExitBattle();
    }

    //int CalculateDamage()
    //{
    //    bool crit = (cs.critChance > Random.Range(0, 100)) ? true : false;
    //    float damage = Random.Range(cs.minAtk, cs.maxAtk);
    //    damage = crit ? damage * cs.critMultiplier : damage;
    //    GameObject.Find("PlayerAttack").GetComponent<TextMeshProUGUI>().text = "Player Attack: " + Mathf.FloorToInt(damage);
    //    GameObject.Find("Critical").GetComponent<TextMeshProUGUI>().text = crit ? "Critical!" : "";
    //    return Mathf.FloorToInt(damage);
    //}

    public void SubmitAction(TurnHandler input)
    {
        performList.Add(input);
    }

    void TakeAction()
    {
        GameObject performer = performList[0].source;
        if (performer.tag == "Enemy")
        {
            EnemiesAI enemy = performer.GetComponent<EnemiesAI>();
            enemy.currentState++;
        }
        else
        {
            currentState++;
        }
    }
}
