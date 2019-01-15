using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public GameObject statusPanel;

    Character chara;
    Enemies enemy;

    [Header("General Settings")]
    public TextMeshProUGUI objectName;
    public TextMeshProUGUI objectType;
    public Image objectSprite;

    [Header("Health & Mana")]
    public Slider healthBar;
    public Slider manaBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;

    void Update()
    {
        if (chara)
        {
            objectName.text = chara.name;
            objectType.text = "Hero";
            objectSprite.sprite = chara.GetComponent<SpriteRenderer>().sprite;

            healthBar.value = Mathf.Clamp(chara.HP / chara.maxHP, 0f, chara.maxHP);
            manaBar.value = Mathf.Clamp(chara.MP / chara.maxMP, 0f, chara.maxMP);

            healthText.text = "HP: " + chara.HP + " / " + chara.maxHP;
            manaText.text = "MP: " + chara.MP + " / " + chara.maxMP;
        }

        else if (enemy)
        {
            objectName.text = enemy.name;
            objectType.text = "Enemy";
            objectSprite.sprite = enemy.GetComponent<SpriteRenderer>().sprite;

            healthBar.value = Mathf.Clamp(enemy.HP / enemy.maxHP, 0f, 1f);
            manaBar.value = Mathf.Clamp(enemy.MP / enemy.maxMP, 0f, 1f);

            healthText.text = "HP: " + Mathf.Clamp(enemy.HP, 0f, enemy.HP) + " / " + enemy.maxHP;
            manaText.text = "MP: " + Mathf.Clamp(enemy.MP, 0f, enemy.MP) + " / " + enemy.maxMP;
        }
    }

    public void GetObject(Character chara, Enemies enemy)
    {
        this.chara = chara;
        this.enemy = enemy;

        if (!statusPanel.activeSelf)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        statusPanel.SetActive(!statusPanel.activeSelf);
    }
}
