using UnityEngine;

public class EnemiesButton : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SelectEnemy()
    {
        FindObjectOfType<BattleManager>().Target(enemyPrefab);
    }
}