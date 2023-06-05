using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waves))]

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _initialPoolSize = 20;
    [SerializeField] private float _waveDelay = 3.0f;
    [SerializeField] private float _spawnZoneWidth;
    [SerializeField] private float _spawnZoneLength;
    [SerializeField] private Vector3 _spawnZoneCenter;
    [SerializeField] private GameObject _enemyContainer;

    private List<GameObject> _enemyPool;
    private Waves _waves;
    private bool _gameOver = false;
    private int _activeEnemies = 0;
    private Coroutine _enemySpawnCoroutine;
    //private int _currentWave = 1;

    private void Awake()
    {        
        _waves = GetComponent<Waves>();
    }

    private void Start()
    {
        InitializeEnemyPool();
        _enemySpawnCoroutine = StartCoroutine(SpawnWaves());
        _activeEnemies = _enemyPool.FindAll(enemy => enemy.activeSelf).Count;
    }

    private void OnEnable()
    {
        Enemy.OnDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        Enemy.OnDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath() //не всегда прилетает событие
    {
        _activeEnemies--;
        Debug.Log("Враг умер. Осталось активных врагов: " + _activeEnemies);

        if (_activeEnemies <= 0 && _gameOver == false)
        {
            _enemySpawnCoroutine = StartCoroutine(SpawnWaves());
        }
        else
        {
            if (_enemySpawnCoroutine != null)
            {
                StopCoroutine(_enemySpawnCoroutine);
            }
        }
    }

    private void InitializeEnemyPool()
    {
        _enemyPool = new List<GameObject>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.SetActive(false);
            enemy.transform.SetParent(_enemyContainer.transform);
            _enemyPool.Add(enemy);
        }
    }

    private GameObject GetEnemyFromPool()
    {
        foreach (GameObject enemy in _enemyPool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.gameObject.SetActive(false);
        newEnemy.transform.SetParent(_enemyContainer.transform);
        _enemyPool.Add(newEnemy);
        return newEnemy;
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(_waveDelay);

        SpawnEnemyWave();

        yield break;
    }

    private void SpawnEnemyWave()
    {        
        int enemyCount = _waves.GetEnemyCount();
        int enemyHealth = _waves.GetEnemyHealth();
        int enemyAttack = _waves.GetEnemyAttack();

        _waves.AdvanceToNextWave();

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = GetEnemyFromPool();

            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().Initialize(enemyHealth, enemyAttack);
                enemy.GetComponent<EnemyMovement>().StartMoving();
                enemy.transform.position = GetRandomSpawnPoint();
                enemy.SetActive(true);
            }
        }

        _activeEnemies = _enemyPool.FindAll(enemy => enemy.activeSelf).Count;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float x = _spawnZoneCenter.x + Random.Range(-_spawnZoneWidth / 2, _spawnZoneWidth / 2);
        float z = _spawnZoneCenter.z + Random.Range(-_spawnZoneLength / 2, _spawnZoneLength / 2);

        return new Vector3(x, _spawnZoneCenter.y, z);
    }

    private void GameOver() //добавить чек события гейм овера
    {
        _gameOver = true;
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(_spawnZoneCenter, new Vector3(_spawnZoneWidth, 0, _spawnZoneLength));
    }
}
