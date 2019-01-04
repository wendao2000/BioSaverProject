using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies", menuName = "Enemies")]
public class ScriptEnemies : ScriptableObject
{
    public int enemiesID = -1;
    public string enemiesName = "Enemies";
    public Sprite enemiesSprite = null;
    public int enemiesHP = 0;
    public int enemiesMP = 0;
    public float enemiesATK = 0f;
    public float enemiesDEF = 0f;
    public float enemiesFLEE = 0f;
    public int enemiesEXP = 0;
}

[Serializable]
public class EnemiesList
{
    public ScriptEnemies enemy;
}