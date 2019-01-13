using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    CharacterEquipment ce;

    public int LVL;
    private int curLVL;

    private int EXP;

    private int offsetMinAtk, offsetMaxAtk;

    [HideInInspector] public float minAtk, maxAtk;
    [HideInInspector] public float maxHP, maxMP;

    [HideInInspector] public float HP;
    [HideInInspector] public float MP;

    [HideInInspector] public float ATK;
    [HideInInspector] public float DEF;

    [HideInInspector] public float CRIT;
    [HideInInspector] public float CRIT_MULTIPLIER;

    void Awake()
    {
        ce = FindObjectOfType<CharacterEquipment>();
    }

    private void Start()
    {
        curLVL = LVL;
    }

    void Update()
    {
        if (curLVL != LVL)
        {
            HP = Mathf.Floor(HP / maxHP * BaseHealth());
            MP = Mathf.Floor(MP / maxMP * BaseMana());
            curLVL = LVL;            
        }

        maxHP = BaseHealth();
        maxMP = BaseMana();

        offsetMinAtk = Mathf.FloorToInt(1f + LVL * 0.4f);
        offsetMaxAtk = Mathf.FloorToInt(3f + LVL * 0.6f);

        minAtk = BaseAttack() - offsetMinAtk + (ce.wep ? ce.wep.minAtk : 0);
        maxAtk = BaseAttack() + offsetMaxAtk + (ce.wep ? ce.wep.maxAtk : 0);

        ATK = (minAtk + maxAtk) / 2;

        DEF = BaseDefense() + (ce.arm ? ce.arm.armorValue : 0);

        CRIT = BaseCritChance() > (ce.wep ? ce.wep.critChance : 0) ? BaseCritChance() : (ce.wep ? ce.wep.critChance : 0);

        CRIT_MULTIPLIER = BaseCritMultiplier() + (ce.wep ? ce.wep.critMultiplier : 0);
    }

    #region ALGORITHM

    private float BaseHealth()
    {
        float baseHealth = 30f;
        float modifier = 8f;
        return Mathf.Floor(baseHealth + (LVL * modifier));
    }

    private float BaseMana()
    {
        float baseMana = 8f;
        float modifier = 3f;
        return Mathf.Floor(baseMana + (LVL * modifier));
    }

    private float BaseAttack()
    {
        float baseAtk = 12f;
        float modifier = 2.6f;
        return Mathf.Floor(baseAtk + (LVL * modifier));
    }

    private float BaseDefense()
    {
        float baseArmor = 4f;
        float modifier = 1.2f;
        return Mathf.Floor(baseArmor + LVL * modifier);
    }

    private float BaseCritChance()
    {
        int baseCritChance = 5;
        float modifier = .24f;
        return Mathf.Floor(Mathf.Clamp(baseCritChance + LVL * modifier, 0f, 40f));
    }

    private float BaseCritMultiplier()
    {
        float baseCritMultiplier = 1.2f;
        float modifier = .04f;
        return Mathf.Floor(Mathf.Clamp(baseCritMultiplier + (LVL * modifier), 1f, 5f));
    }

    #endregion

    public void GainExperience(int value)
    {
        EXP += value;
    }

    public void SkillAvailable()
    {

    }

    private void OnMouseDown() //BattleMode
    {
        if (FindObjectOfType<BattleManager>())
        {
            FindObjectOfType<StatusManager>().GetObject(this, null);

            if (!FindObjectOfType<StatusManager>().statusPanel.activeSelf)
            {
                FindObjectOfType<ButtonManager>().ToggleStatusPanel();
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LVL = PlayerPrefs.HasKey("pLevel") ? PlayerPrefs.GetInt("pLevel", 1) : 1;

        HP = PlayerPrefs.HasKey("pCurHealth") ? PlayerPrefs.GetInt("pCurHealth", (int)maxHP) : BaseHealth();
        MP = PlayerPrefs.HasKey("pCurMana") ? PlayerPrefs.GetInt("pCurMana", (int)maxMP) : BaseMana();
    }
}
