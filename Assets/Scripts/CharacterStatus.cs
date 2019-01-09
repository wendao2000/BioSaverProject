using System;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    CharacterEquipment ce;

    public int LVL;

    private int EXP;

    [HideInInspector] public int minAtk, maxAtk;

    private int offsetMinAtk, offsetMaxAtk;

    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int CRIT;
    public float CRIT_MULTIPLIER;

    void Awake()
    {
        ce = FindObjectOfType<CharacterEquipment>();
    }

    void Start()
    {
        LVL = PlayerPrefs.HasKey("pLevel") ? PlayerPrefs.GetInt("pLevel", 1) : 1;
    }

    void Update()
    {
        offsetMinAtk = Mathf.FloorToInt(1f + LVL * 0.4f);
        offsetMaxAtk = Mathf.FloorToInt(3f + LVL * 0.6f);

        minAtk = BaseAttack() - offsetMinAtk + (ce.wep ? ce.wep.minAtk : 0);
        maxAtk = BaseAttack() + offsetMaxAtk + (ce.wep ? ce.wep.maxAtk : 0);

        HP = BaseHealth();
        MP = BaseMana();
        ATK = (minAtk + maxAtk) / 2;
        DEF = BaseDefense() + (ce.arm ? ce.arm.armorValue : 0);
        CRIT = BaseCritChance() > (ce.wep ? ce.wep.critChance : 0) ? BaseCritChance() : (ce.wep ? ce.wep.critChance : 0);
        CRIT_MULTIPLIER = BaseCritMultiplier() + (ce.wep ? ce.wep.critMultiplier : 0);
    }

    private int BaseHealth()
    {
        float baseHealth = 30f;
        float modifier = 8f;
        return Mathf.FloorToInt(baseHealth + (LVL * modifier));

    }

    private int BaseMana()
    {
        float baseMana = 8f;
        float modifier = 3f;
        return Mathf.FloorToInt(baseMana + (LVL * modifier));
    }    

    int BaseAttack()
    {
        float baseAtk = 12f;
        float modifier = 2.6f;
        return Mathf.FloorToInt(baseAtk + (LVL * modifier));
    }

    int BaseDefense()
    {
        float baseArmor = 4f;
        float modifier = 1.2f;
        return Mathf.FloorToInt(baseArmor + LVL * modifier);
    }

    int BaseCritChance()
    {
        int baseCritChance = 5;
        float modifier = .24f;
        return Mathf.FloorToInt(Mathf.Clamp(baseCritChance + LVL * modifier, 0f, 40f));
    }

    int BaseCritMultiplier()
    {
        float baseCritMultiplier = 1.2f;
        float modifier = .04f;
        return Mathf.FloorToInt(Mathf.Clamp(baseCritMultiplier + (LVL * modifier), 1f, 5f));
    }

}
