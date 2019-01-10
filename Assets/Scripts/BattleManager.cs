﻿using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    GameManager gm;

    public GameObject endBattlePanel;

    public enum BattleState
    {
        IDLE,
        PERFORMACTION,
        ENDBATTLE
    }

    public enum HeroGUI
    {
        IDLE,
        ACTIVATE,
        INPUT,
        DONE
    }

    public BattleState currentState;
    public HeroGUI heroInput;

    [HideInInspector] public int heroDead, enemyDead;

    //prefab
    public GameObject enemyButton;

    public List<TurnHandler> performList = new List<TurnHandler>();

    //static list
    public List<GameObject> heroList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();

    //dynamic queue
    public List<GameObject> heroQueue = new List<GameObject>();

    TurnHandler heroMove = new TurnHandler();

    void Start()
    {
        gm = GameManager.GetInstance();

        gm.inBattle = true;

        currentState = BattleState.IDLE;
        heroInput = HeroGUI.IDLE;

        heroList.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        if(heroDead == heroList.Count || enemyDead == enemyList.Count)
        {
            currentState = BattleState.ENDBATTLE;
        }

        switch (currentState)
        {
            case (BattleState.IDLE):
                if (performList.Count > 0)
                {
                    currentState = BattleState.PERFORMACTION;
                }
                break;
            case (BattleState.PERFORMACTION):
                PerformAction();
                break;

            case (BattleState.ENDBATTLE):
                EndBattle(heroDead == heroList.Count ? "LOSE" : "WIN");
                break;
        }

        switch (heroInput)
        {
            case (HeroGUI.IDLE):
                //idle, wait for cooldown
                if (heroQueue.Count > 0)
                {
                    heroInput = HeroGUI.ACTIVATE;
                }
                break;

            case (HeroGUI.ACTIVATE):
                //activating game object
                if (performList.Count == 0)
                {
                    heroQueue[0].transform.Find("Selector").gameObject.SetActive(true);
                    gm.mainPanel.gameObject.SetActive(true);
                    heroInput = HeroGUI.INPUT;
                }
                break;

            case (HeroGUI.INPUT):
                //wait for input
                break;

            case (HeroGUI.DONE):
                HeroSubmit();
                break;
        }
    }

    private void GenerateEnemies()
    {

    }

    public void Attack()
    {
        heroMove.source = heroQueue[0];
        gm.secondPanel.gameObject.SetActive(true);
        gm.secondPanel.GetComponent<BMSecondPanel>().SecondPanel("Attack");
    }

    public void Magic()
    {
        heroMove.source = heroQueue[0];
        gm.secondPanel.gameObject.SetActive(true);
        gm.secondPanel.GetComponent<BMSecondPanel>().SecondPanel("Magic");
    }

    public void UseItem()
    {
        heroMove.source = heroQueue[0];
        gm.secondPanel.gameObject.SetActive(true);
        gm.secondPanel.GetComponent<BMSecondPanel>().SecondPanel("Item");
    }

    public void Flee()
    {

    }

    public void Target(GameObject obj)
    {
        heroMove.target = obj;
        heroQueue[0].GetComponent<CharacterAI>().currentState++;
        heroInput++;
    }

    void HeroSubmit()
    {
        performList.Add(heroMove);
        heroQueue[0].transform.Find("Selector").gameObject.SetActive(false);
        heroQueue.RemoveAt(0);

        gm.mainPanel.gameObject.SetActive(false);
        gm.secondPanel.gameObject.SetActive(false);

        heroInput = HeroGUI.IDLE;
    }

    public void SubmitAction(TurnHandler input)
    {
        performList.Add(input);
    }

    void PerformAction()
    {
        GameObject performer = performList[0].source;
        if (performer.tag == "Enemy")
        {
            EnemiesAI enemy = performer.GetComponent<EnemiesAI>();
            enemy.currentState = EnemiesAI.EnemyState.BATTLEPHASE;
        }
        else if (performer.tag == "Hero")
        {
            CharacterAI hero = performer.GetComponent<CharacterAI>();
            hero.currentState = CharacterAI.HeroState.BATTLEPHASE;
        }
        currentState = BattleState.IDLE;
    }

    private void EndBattle(string result)
    {
        switch (result)
        {
            case ("WIN"):
                endBattlePanel.SetActive(true);
                break;

            case ("LOSE"):
                endBattlePanel.SetActive(true);
                break;
        }
    }

    public void SpawnEnemyButton()
    {
        foreach (GameObject enemy in enemyList)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemiesButton btn = newButton.GetComponent<EnemiesButton>();

            Enemies curEnemy = enemy.GetComponent<Enemies>();
            TextMeshProUGUI btnText = newButton.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
            newButton.name = btnText.text = curEnemy.name;

            btn.enemyPrefab = enemy;

            newButton.transform.SetParent(gm.secondPanel.GetComponent<BMSecondPanel>().layout, false);
        }
    }
}
