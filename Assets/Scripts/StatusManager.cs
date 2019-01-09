using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public GameObject statusPanel;
    public GameObject toggleButton;

    Enemies enemy;

    [Header("General Settings")]
    public TextMeshProUGUI objectName;
    public TextMeshProUGUI objectType;

    [Header("Health & Mana")]
    public Slider healthBar;
    public Slider manaBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;

    void Update()
    {
        if (enemy)
        {
            objectName.text = enemy.name;
            objectType.text = "Enemy";

            healthBar.value = Mathf.Clamp(enemy.HP / enemy.maxHP, 0f, enemy.maxHP);
            manaBar.value = Mathf.Clamp(enemy.MP / enemy.maxMP, 0f, enemy.maxMP);

            healthText.text = "HP: " + enemy.HP + " / " + enemy.maxHP;
            manaText.text = "MP: " + enemy.MP + " / " + enemy.maxMP;
        }
    }

    public void GetEnemies(Enemies enemy)
    {
        this.enemy = enemy;
    }
}
