using System.Collections.Generic;
using UnityEngine;
using Walls;

namespace Enemies
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemyContainer;
        [SerializeField] private Wall[] _walls;

        private List<GameObject> _enemiesPool;
        private List<Enemy> _enemies;

        public void Initialize()
        {
            _enemiesPool = new List<GameObject>();
            _enemies = new List<Enemy>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject enemy = Instantiate(_enemyPrefab);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();

                _enemies.Add(enemyScript);

                enemy.SetActive(false);
                enemy.transform.SetParent(_enemyContainer.transform);
                _enemiesPool.Add(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            foreach (GameObject enemy in _enemiesPool)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    return enemy;
                }
            }

            GameObject newEnemy = Instantiate(_enemyPrefab);
            EnemyBehavior enemyBehavior = newEnemy.GetComponent<EnemyBehavior>();

            newEnemy.gameObject.SetActive(false);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            _enemiesPool.Add(newEnemy);
            _enemies.Add(newEnemy.GetComponent<Enemy>());
            return newEnemy;
        }

        public int GetCountActiveEnemies()
        {
            return _enemiesPool.FindAll(enemy => enemy.activeSelf).Count;
        }
    }
}