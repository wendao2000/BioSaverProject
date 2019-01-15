using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    GameManager gm;

    [HideInInspector] public StatusManager statusManager;
    
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

    [HideInInspector] public int heroDead, enemyDead; //check if there's still heroes or enemies available on field

    [Header("Prefab")]
    public GameObject enemy; //spawn enemies using this prefab
    public GameObject enemyButton; //use this button setting

    [Header("Battle Manager")]
    public List<TurnHandler> performList = new List<TurnHandler>(); //current object's turn
    
    public List<GameObject> heroList = new List<GameObject>(); //get all heroes on field
    public List<GameObject> enemyList = new List<GameObject>(); //get all enemies on field
    
    public List<GameObject> heroQueue = new List<GameObject>(); //current hero's turn (if not null, pause cooldown)

    TurnHandler heroMove = new TurnHandler();

    public int expGained = 0;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        statusManager = GetComponent<StatusManager>();

        heroDead = enemyDead = 0; //reset dead count

        GenerateEnemies(); //generate enemies

        currentState = BattleState.IDLE;
        heroInput = HeroGUI.IDLE;

        heroList.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        if (heroDead == heroList.Count || enemyDead == enemyList.Count)
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
                //idle, wait for action
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

    #region ActionButton
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
    #endregion

    #region HeroInput
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
    #endregion

    #region BattleState
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
        endBattlePanel.SetActive(true);
        endBattlePanel.GetComponent<Animator>().SetTrigger("Activate");

        switch (result)
        {
            case ("WIN"):
                expGained = 0;

                foreach (GameObject enemy in enemyList)
                {
                    expGained += enemy.GetComponent<Enemies>().EXP;
                }

                endBattlePanel.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = "You Win";
                endBattlePanel.transform.Find("ExperienceGained").GetComponent<TextMeshProUGUI>().text = "Experience Gained: " + expGained;
                break;

            case ("LOSE"):
                endBattlePanel.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = "You Lose";
                endBattlePanel.transform.Find("ExperienceGained").GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
    }
    #endregion

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

    private void GenerateEnemies()
    {
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = new Vector2(6.5f, 1.5f);
        newEnemy.GetComponent<Enemies>().source = gm.GetEnemies(PlayerPrefs.GetInt("EnemiesID"));
    }
}
