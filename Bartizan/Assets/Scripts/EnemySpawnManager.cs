using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] private Transform spawnLocation;

    private int baseEnemies = 5;
    private float enemiesPerSecond = 0.5f;
    private float timeBetweenWaves = 3f;
    private float waveMultiplier = 0.75f;

    [SerializeField] private TMPro.TextMeshProUGUI waveText;
    private int currWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private GameObject spawnedEnemy;
    private List<Vector3> enemy_Path;

    private bool startRound;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        onEnemyDestroy.AddListener(OnEnemyDestroyed);
        startRound = false;

    }
    public void setPath(List<Vector3> path, GameObject start)
    {
        enemy_Path = path;
    }

    private void Update()
    {
        if (!isSpawning) return; // if not spawning, don't do anything
        if (startRound) // if the round has started, spawn enemies
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= (1 / enemiesPerSecond) && enemiesLeftToSpawn > 0)
            {
                Debug.Log("Spawn Enemy");
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0;
            }

            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currWave++;
        waveText.text = "Wave " + currWave;
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {

        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("Starting New Wave this is Wave: " + currWave);
        isSpawning = true;
        enemiesLeftToSpawn = CalculateEnemiesPerWave();
    }

    private int CalculateEnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currWave, waveMultiplier));
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        spawnedEnemy = Instantiate(prefabToSpawn, spawnLocation.GetComponent<Transform>().position, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().setup(enemy_Path);
    }

    private void OnEnemyDestroyed()
    {
        Debug.Log("Enemy has been Destroyed (Invoke)");
        enemiesAlive--;
    }

    public void beginRound()
    {
        startRound = true;
        StartCoroutine(StartWave());// start the first wave
    }

    public int getCurrWave()
    {
        return currWave;
    }

    public int getEnemiesAlive()
    {
        return enemiesAlive;
    }
}
