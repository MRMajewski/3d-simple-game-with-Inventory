using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnIntervalMin = 3f;
    [SerializeField] private float spawnIntervalMax = 8f;
    [SerializeField] private Transform playerTransform;

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitUntil(() => gameController != null && gameController.IsGameActive());

            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            if (gameController.IsGameActive()) 
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                EnemyAI newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                newEnemy.Target = playerTransform;
                newEnemy.gameObject.SetActive(true);
            }
        }
    }
}
