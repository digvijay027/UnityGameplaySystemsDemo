using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float nextWaveDelay = 2f;
    [SerializeField] private float spawnRadius = 15f;

    private int currentWave;
    private int enemiesAlive;
    private bool spawningWave;

    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (spawningWave)
        {
            return;
        }

        currentWave++;

        StartCoroutine(SpawnWave());
    }

    public int CurrentWave
    {
        get { return currentWave; }
    }

    public int EnemiesAlive
    {
        get { return enemiesAlive; }
    }

    private IEnumerator SpawnWave()
    {
        spawningWave = true;

        int enemyCount = enemiesPerWave + (currentWave - 1);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnDelay);
        }

        spawningWave = false;
    }

    private void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = new Vector3(
            randomDirection.x * spawnRadius,
            1f,
            randomDirection.y * spawnRadius
        );

        EnemyController enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.SetSpawner(this);
        }

        enemiesAlive++;
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !spawningWave)
        {
            StartCoroutine(StartNextWaveAfterDelay());
        }
    }

    private IEnumerator StartNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(nextWaveDelay);

        StartNextWave();
    }
}