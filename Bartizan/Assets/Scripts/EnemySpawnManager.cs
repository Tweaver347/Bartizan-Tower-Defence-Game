using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    
    private Transform spawnLocation;

    private int baseEnemies = 5;
    private float enemiesPerSecond = 0.5f;
    private float timeBetweenWaves = 3f;
    private float waveMultiplier = 0.75f;

    private int currWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private GameObject spawnedEnemy;
    private List<Vector3> enemy_Path;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        onEnemyDestroy.AddListener(OnEnemyDestroyed);

    }

    void Start()
    {
        // Temp before will turn into a method where the player can start the first round
        StartCoroutine(StartWave());
    }

    public void setPath(List<Vector3> path, GameObject start)
    {
        enemy_Path = path;
        spawnLocation = start.GetComponent<Transform>();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1/enemiesPerSecond) && enemiesLeftToSpawn > 0)
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

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currWave++;
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
        spawnedEnemy =  Instantiate(prefabToSpawn, spawnLocation.GetComponent<Transform>().position, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().setup(enemy_Path);
    }

    private void OnEnemyDestroyed()
    {
        Debug.Log("Enemy has been Destroyed (Invoke)");
        enemiesAlive--;
    }
}
