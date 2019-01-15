using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharaBriefInfo : MonoBehaviour
{
    GameManager gm;

    [Header("Level & Sprite")]
    public TextMeshProUGUI level;
    public Image sprite;

    [Header("Health & Mana")]
    public Slider healthBar;
    public Slider manaBar;
    public Slider EXPBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI EXPText;

    [Header("Money")]
    public TextMeshProUGUI money;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        level.text = gm.chara.LVL.ToString();
        sprite.sprite = gm.chara.GetComponent<SpriteRenderer>().sprite;
        healthBar.value = Mathf.Clamp(gm.chara.HP / gm.chara.maxHP, 0f, gm.chara.maxHP);
        manaBar.value = Mathf.Clamp(gm.chara.MP / gm.chara.maxMP, 0f, gm.chara.maxMP);
        EXPBar.value = Mathf.Clamp(gm.chara.EXP / gm.chara.nextEXP, 0f, gm.chara.nextEXP);

        healthText.text = "HP: " + gm.chara.HP + " / " + gm.chara.maxHP;
        manaText.text = "MP: " + gm.chara.MP + " / " + gm.chara.maxMP;
        EXPText.text = "EXP: " + gm.chara.EXP + " / " + gm.chara.nextEXP;

        money.text = gm.currentMoney.ToString();
    }
}
