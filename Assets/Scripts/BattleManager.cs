using UnityEngine;

public class BattleManager : MonoBehaviour {

    //Equipment eq; //character equipment
    //GameObject en; //enemies
    GameManager gm;

    void Start()
    {
        //eq = FindObjectOfType<Equipment>();
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        //en.healthPoint -= CalculateDamage();
    }

    public void GenerateEnemies(int[] index)
    {
        //get index and Generate Enemies
    }

    public void Attack()
    {
        //gm.secondPanel.SetActive(true);
    }

    public void Magic()
    {
        gm.secondPanel.SetActive(true);

    }

    public void UseItem()
    {
        //gm.secondPanel.SetActive(true);
    }

    public void Flee()
    {
        //using random value, if satisfy, successfully escaped, vice versa
        gm.ExitBattle();
    }

    //float CalculateDamage()
    //{
    //bool crit = Random.Range(0, 100) > critChance ? true : false;
    //float damage = Random.Range(eq.minAtk, eq.maxAtk);
    //damage = crit ? damage * eq.critMultiplier : damage;

    //return damage;
    //}
}
