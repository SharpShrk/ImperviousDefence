using System;
using System.Collections;
using UnityEngine;
using WalletAndScore;
using Wave;

namespace Enemies
{
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

        public event Action WaveCleared;

        public int ActiveEnemies => _activeEnemies;

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

            if (_activeEnemies <= 0 && _gameOver == false)
            {
                WaveCleared?.Invoke();
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
                    enemyScript.HideHealthBar();
                    enemy.transform.position = GetRandomSpawnPoint();
                    enemy.SetActive(true);
                    _attackPointQueue.AddEnemyToQueue(enemyScript);
                }
            }

            _activeEnemies = _enemyPool.GetCountActiveEnemies();
        }

        private Vector3 GetRandomSpawnPoint()
        {
            float SpawnZoneWidtMultiplier = 0.5f;
            float SpawnZoneLengthMultiplier = 0.5f;

            float halfWidth = _spawnZoneWidth * SpawnZoneWidtMultiplier;
            float halfLength = _spawnZoneLength * SpawnZoneLengthMultiplier;

            float x = _spawnZoneCenter.x + UnityEngine.Random.Range(-halfWidth, halfWidth);
            float z = _spawnZoneCenter.z + UnityEngine.Random.Range(-halfLength, halfLength);

            return new Vector3(x, _spawnZoneCenter.y, z);
        }

        private void GameOver()
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
}