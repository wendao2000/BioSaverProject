using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public CharacterMovement charaMove;
    [HideInInspector] public CharacterInventory charaInven;

    GameManager gm;

    CharacterEquipment ce;

    public int LVL;
    private int curLVL = 0; //check if level changed
    
    [HideInInspector] public float EXP, nextEXP;

    [HideInInspector] public float maxHP, maxMP;
    [HideInInspector] public float minAtk, maxAtk;

    [Header("Character Information")]
    public float HP, MP;
    public float ATK, DEF;
    public float CRIT, CRIT_MULTIPLIER;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        ce = FindObjectOfType<CharacterEquipment>();

        LVL = PlayerPrefs.GetInt("pLevel", 1);
        HP = PlayerPrefs.GetInt("pCurHealth", (int)BaseHealth());
        MP = PlayerPrefs.GetInt("pCurMana", (int)BaseMana());
        EXP = PlayerPrefs.GetInt("pCurEXP", 0);
        nextEXP = 25;

        maxHP = BaseHealth();
        maxMP = BaseMana();
    }

    void Update()
    {
        if (curLVL != LVL) //increase status if level up
        {
            HP = Mathf.Floor(HP / maxHP * BaseHealth());
            MP = Mathf.Floor(MP / maxMP * BaseMana());

            maxHP = BaseHealth();
            maxMP = BaseMana();

            minAtk = BaseAttack() + (ce.wep ? ce.wep.minAtk : 0);
            maxAtk = BaseAttack() + (ce.wep ? ce.wep.maxAtk : 0);

            ATK = (minAtk + maxAtk) / 2;

            DEF = BaseDefense() + (ce.arm ? ce.arm.armorValue : 0);

            CRIT = BaseCritChance() > (ce.wep ? ce.wep.critChance : 0) ? BaseCritChance() : (ce.wep ? ce.wep.critChance : 0);

            CRIT_MULTIPLIER = BaseCritMultiplier() + (ce.wep ? ce.wep.critMultiplier : 0);

            curLVL = LVL;
        }
    }

    #region TemporaryAlgorithm
    private float BaseHealth()
    {
        float baseHealth = 32f;
        float modifier = 8f;
        return Mathf.Floor(baseHealth + (LVL * modifier));
    }

    private float BaseMana()
    {
        float baseMana = 10f;
        float modifier = 2f;
        return Mathf.Floor(baseMana + (LVL * modifier));
    }

    private float BaseAttack()
    {
        float baseAtk = 16f;
        float modifier = 4f;
        return Mathf.Floor(baseAtk + (LVL * modifier));
    }

    private float BaseDefense()
    {
        float baseArmor = 4f;
        float modifier = 2f;
        return Mathf.Floor(baseArmor + LVL * modifier);
    }

    private float BaseCritChance()
    {
        int baseCritChance = 5;
        float modifier = 0.2f;
        return Mathf.Floor(Mathf.Clamp(baseCritChance + LVL * modifier, 0f, 40f));
    }

    private float BaseCritMultiplier()
    {
        float baseCritMultiplier = 1.2f;
        float modifier = .1f;
        return Mathf.Floor(Mathf.Clamp(baseCritMultiplier + (LVL * modifier), 1f, 5f));
    }
    #endregion

    public void GainExperience(int value)
    {
        EXP += value;
        if (EXP > nextEXP)
        {
            LVL++;
            EXP -= nextEXP;
            //nextEXP = //check for EXPTable;
        }
    }

    public void SkillAvailable() //Magic
    {

    }

    private void OnMouseDown() //BattleMode
    {
        if (gm.battleManager != null)
        {
            gm.battleManager.statusManager.GetObject(this, null);
        }
    }
}
