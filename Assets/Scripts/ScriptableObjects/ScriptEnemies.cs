using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies", menuName = "Enemies")]
public class ScriptEnemies : ScriptableObject
{
    public int enemiesID = -1;
    public string enemiesName = "Enemies";
    public Sprite enemiesSprite = null;
    public float enemiesAttack = 0;
    public float enemiesDefense = 0;
    public float enemiesFleeChance = 0;
    public int enemiesEXPGiven = 0;
}

[Serializable]
public class EnemiesList
{
    public ScriptEnemies enemy;
}