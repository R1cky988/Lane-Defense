using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
   
    [System.Serializable]
    public class EnemyPrefabByType
    {
        public EnemyType type;
        public GameObject prefab;
    }
     [Header("References")]
    [SerializeField] private EnemyPrefabByType[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int totalEnemies = 8;
    [SerializeField] private float enemyPerSecond = 1f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float timeBetweenWave = 5f;
    [SerializeField] private float difficultyMultiplier = 0.75f;

    public int currentWave = 1;
    [SerializeField]private float timeLastSpawned;
    [SerializeField]private int enemyAlive;
   [SerializeField] private int enemyLeftToSpawn;
    [SerializeField]private bool isSpawning = false;

    

    private List<EnemyType> enemiesToSpawn = new List<EnemyType>();

    public static UnityEvent onEnemyDestroyed = new UnityEvent();

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Awake()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    void Update()
    {
        if (isSpawning == false)
        {
            return;
        }
        timeLastSpawned += Time.deltaTime;
        if(timeLastSpawned >= (1f/ enemyPerSecond) && enemyLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemyLeftToSpawn--;
            enemyAlive++;
            timeLastSpawned = 0f;
        }

        if(enemyLeftToSpawn == 0 && enemyAlive == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        LevelManager.instance.OnWaveCompleted(currentWave);
        currentWave++;
        timeLastSpawned = 0f;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemyAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWave);

        PlayerShoot.instance.RefillBullets();
        
        isSpawning = true;
        enemyAlive = 0;
        enemyLeftToSpawn = EnemyPerWave();
        prepareWave();
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, LevelManager.instance.enemyPoints.Length);
        EnemyType typeToSpawn = enemiesToSpawn[0];
        enemiesToSpawn.RemoveAt(0);
        GameObject enemyToSpawn = GetPrefabByType(typeToSpawn);
        Instantiate(enemyToSpawn, LevelManager.instance.enemyPoints[randomIndex].position, Quaternion.identity);
    }
    private int EnemyPerWave()
    {
        return Mathf.RoundToInt(totalEnemies * Mathf.Pow(currentWave, difficultyMultiplier));
    }

    private void prepareWave()
    {
        enemiesToSpawn.Clear();
        int enemyCount = EnemyPerWave();
        if(currentWave < 3)
        {
            for(int i = 0; i < enemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Basic);
            }
        }
        else if(currentWave % 5 == 0)
        {
            int strongEnemyCount = Mathf.FloorToInt(enemyCount * 0.6f);
            int tankEnemyCount = Mathf.FloorToInt(enemyCount * 0.2f);
            int basicEnemyCount = enemyCount - strongEnemyCount - tankEnemyCount;
            
            for(int i = 0; i < basicEnemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Basic);
            }
            for(int i = 0; i < tankEnemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Tank);
            }
            for(int i = 0; i < strongEnemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Strong);
            }
        }
        else
        {
            int strongEnemyCount = Mathf.FloorToInt(enemyCount * 0.4f);
            int basicEnemyCount = enemyCount - strongEnemyCount;

            for(int i = 0; i < basicEnemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Basic);
            }
            for(int i = 0; i < strongEnemyCount; i++)
            {
                enemiesToSpawn.Add(EnemyType.Strong);
            }
        }
        Shuffle(enemiesToSpawn);
    }
    private void Shuffle(List<EnemyType> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
    private GameObject GetPrefabByType(EnemyType type)
    {
        foreach (var e in enemyPrefabs)
        {
            if (e.type == type)
                return e.prefab;
        }
        return null;
    }
}
