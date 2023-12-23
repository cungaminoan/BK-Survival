using UnityEngine;

public class SC_EnemySpawner : MonoBehaviour
{
    public GameObject droneEnemyPrefab;
    public Transform[] droneSpawnPoints;
    private int totalEnemiesSpawned = 0;
    private const int MaxEnemies = 30;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization if needed
    }

    // Update is called once per frame
    void Update()
    {
        if (totalEnemiesSpawned < MaxEnemies)
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        Transform randomPoint = droneSpawnPoints[Random.Range(0, droneSpawnPoints.Length)];
        GameObject droneEnemy = Instantiate(droneEnemyPrefab, randomPoint.position, Quaternion.identity);
        SC_NPCEnemy npc = droneEnemy.GetComponent<SC_NPCEnemy>();
        npc.es = this;
        FlockManager.FM.allFish.Add(droneEnemy);
        totalEnemiesSpawned++;
    }

    public void EnemyEliminated(SC_NPCEnemy enemy)
    {
        FlockManager.FM.allFish.Remove(enemy.gameObject);
    }
}
