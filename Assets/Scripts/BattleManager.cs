using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{

    CharacterStatus cs;
    GameManager gm;

    public int enemiesHP;
    public int enemiesMP;
    public float enemiesAtk;
    public float enemiesDef;
    public float fleeChance;
    public int enemiesEXPGiven;

    void Awake()
    {
        gm = GameManager.GetInstance();
        cs = FindObjectOfType<CharacterStatus>();
    }

    void Start()
    {
        GenerateEnemies(PlayerPrefs.GetInt("EnemiesID"));
    }

    void Update()
    {
        GameObject.Find("EnemiesHP").GetComponent<TextMeshProUGUI>().text = "Enemies HP: " + enemiesHP;
        if (enemiesHP <= 0)
        {
            

            Debug.Log("Enemies Defeated!");
            Debug.Log("Gained " + enemiesEXPGiven + " Experience Points");
        }
    }

    private void GenerateEnemies(int index)
    {
        
    }

    public void Attack()
    {
        int playerAtk = CalculateDamage();
        int damageGiven = Mathf.FloorToInt((playerAtk * playerAtk) / (playerAtk + enemiesDef));
        GameObject.Find("DamageGiven").GetComponent<TextMeshProUGUI>().text = "Damage Given: " + Mathf.FloorToInt(damageGiven);
        enemiesHP = Mathf.Clamp(enemiesHP - damageGiven, 0, enemiesHP);
    }

    public void Magic()
    {
        if (gm.itemPanel.activeSelf)
            {
                gm.itemPanel.SetActive(false);
            }

        gm.magicPanel.SetActive(true);
    }

    public void UseItem()
    {
        if (gm.magicPanel.activeSelf)
            {
                gm.magicPanel.SetActive(false);
            }

        gm.itemPanel.SetActive(true);
    }

    public void Flee()
    {
        //using random value, if satisfy, successfully escaped, vice versa
        gm.ExitBattle();
    }

    int CalculateDamage()
    {
        bool crit = (cs.critChance > Random.Range(0, 100)) ? true : false;
        float damage = Random.Range(cs.minAtk, cs.maxAtk);
        damage = crit ? damage * cs.critMultiplier : damage;
        GameObject.Find("PlayerAttack").GetComponent<TextMeshProUGUI>().text = "Player Attack: " + Mathf.FloorToInt(damage);
        GameObject.Find("Critical").GetComponent<TextMeshProUGUI>().text = crit ? "Critical!" : "";
        return Mathf.FloorToInt(damage);
    }
}
