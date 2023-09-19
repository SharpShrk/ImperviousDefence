using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waves))]

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _waveDelay = 3.0f;
    [SerializeField] private float _spawnZoneWidth;
    [SerializeField] private float _spawnZoneLength;
    [SerializeField] private Vector3 _spawnZoneCenter;
    [SerializeField] private RewardCollector _rewardCollector;
    [SerializeField] private EnemyPool _enemyPool;

    private AttackPointQueue _attackPointQueue;
    private Waves _waves;
    private bool _gameOver = false;
    private int _activeEnemies = 0;
    private Coroutine _enemySpawnCoroutine;
    private FreeAttackPointChecker _attackPointChecker;

    public int ActiveEnemies => _activeEnemies;


    //добавить такой функционал. Если врагов спавнится больше, чем занято точек, то враги должны ожидать своей очереди на спавен
    //подумать над исполнением, как это лучше реализовать. Может добавлять их в какой то лист и после каждой смерти врага проверять сколько осталось их в листе
    //еще есть другая проблема. Когда врагов спавнится больше 3 на одну стену, то свободных точек для них нет, возможно стоит предложить им пойти к другой точке другой стены
    //Но из этого тоже может выкатиться проблема, что враг с одного края пойдет к другому концу карты и может зацепиться за двери или за башни или вообще криво встанет

    private void Awake()
    {
        _attackPointQueue = GetComponent<AttackPointQueue>();
        _waves = GetComponent<Waves>();
        _attackPointChecker = GetComponent<FreeAttackPointChecker>();
    }

    private void Start()
    {
        _enemyPool.InitializeEnemyPool();
        _enemySpawnCoroutine = StartCoroutine(SpawnWaves());
        _activeEnemies = _enemyPool.GetCountActiveEnemies();
        
    }

    private void CheckSpawnedEnemy()
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
            GameObject enemy = _enemyPool.GetEnemyFromPool();
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.OnEnemyDied += OnEnemyDied;
                enemyScript.Initialize(enemyHealth, enemyAttack);
                enemy.transform.position = GetRandomSpawnPoint();
                enemy.SetActive(true);
                _attackPointQueue.AddEnemyToQueue(enemyScript);
            }
        }

        _activeEnemies = _enemyPool.GetCountActiveEnemies();
    }

    /*private void SpawnEnemyWave()
    {        
        int enemyCount = _waves.GetEnemyCount();
        int enemyHealth = _waves.GetEnemyHealth();
        int enemyAttack = _waves.GetEnemyAttack();

        _waves.AdvanceToNextWave();

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = _enemyPool.GetEnemyFromPool();

            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                if (enemyScript != null)
                {
                    enemyScript.OnEnemyDied += OnEnemyDied;
                }

                enemyScript.Initialize(enemyHealth, enemyAttack);
                enemy.GetComponent<EnemyMovement>().StartMoving();
                enemy.transform.position = GetRandomSpawnPoint();
                enemy.SetActive(true);
                enemyScript.Activate();
            }
        }

        _activeEnemies = _enemyPool.GetCountActiveEnemies();
    }*/

    private Vector3 GetRandomSpawnPoint()
    {
        float x = _spawnZoneCenter.x + Random.Range(-_spawnZoneWidth / 2, _spawnZoneWidth / 2);
        float z = _spawnZoneCenter.z + Random.Range(-_spawnZoneLength / 2, _spawnZoneLength / 2);

        return new Vector3(x, _spawnZoneCenter.y, z);
    }

    private void GameOver() //добавить чек события гейм овера? 
    {
        _gameOver = true;
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(_spawnZoneCenter, new Vector3(_spawnZoneWidth, 0, _spawnZoneLength));
    }

    private void OnEnemyDied(int rewardMoney, int rewardScore, Enemy enemy)
    {
        CheckSpawnedEnemy();
        _rewardCollector.TakeReward(rewardMoney, rewardScore);

        enemy.OnEnemyDied -= OnEnemyDied;
    }
}
