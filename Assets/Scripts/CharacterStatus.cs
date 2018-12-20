using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    CharacterEquipment ce;

    public int level;
    public float experience;

    [HideInInspector] public float minAtk, maxAtk;

    public float offsetMinAtk = 0f;
    public float offsetMaxAtk = 0f;

    public float attackPoint;
    public float defensePoint;
    public int critChance;
    public float critMultiplier;

    void Awake()
    {
        ce = FindObjectOfType<CharacterEquipment>();
    }

    void Start()
    {
        level = PlayerPrefs.HasKey("pLevel") ? PlayerPrefs.GetInt("pLevel", 1) : 1;
    }

    void Update()
    {
        offsetMinAtk = 1f + level * 0.5f;
        offsetMaxAtk = 3f + level * 0.8f;

        minAtk = BaseAttack() - offsetMinAtk + (ce.wep ? ce.wep.minAtk : 0f);
        maxAtk = BaseAttack() + offsetMaxAtk + (ce.wep ? ce.wep.maxAtk : 0f);
        attackPoint = (minAtk + maxAtk) / 2;
        defensePoint = BaseDefense() + (ce.arm ? ce.arm.armorValue : 0f);
        critChance = BaseCritChance() > (ce.wep ? ce.wep.critChance : 0) ? BaseCritChance() : (ce.wep ? ce.wep.critChance : 0);
        critMultiplier = BaseCritMultiplier() + (ce.wep ? ce.wep.critMultiplier : 0);
    }

    float BaseAttack()
    {
        float baseAtk = 24f;
        float modifier = 3.2f;
        return baseAtk + (level * modifier);
    }

    float BaseDefense()
    {
        float baseArmor = 6f;
        float modifier = 1.8f;
        return baseArmor + level * modifier;
    }

    int BaseCritChance()
    {
        int baseCritChance = 10;
        float modifier = .2f;
        return Mathf.FloorToInt(Mathf.Clamp(baseCritChance + level * modifier, 0f, 40f));
    }

    float BaseCritMultiplier()
    {
        float baseCritMultiplier = 1.5f;
        float modifier = .01f;
        return Mathf.Clamp(baseCritMultiplier + (level * modifier), 1f, 5f);
    }

}
