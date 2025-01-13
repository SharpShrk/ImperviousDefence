using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Walls
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private int _startHealthPoints;
        [SerializeField] private float _ejectionSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _deactivationDelay;

        private int _currentHealthPoints;
        private Wall _wall;
        private int _brickIndex;

        public int BrickIndex => _brickIndex;
        public Vector3 BrickSize => _size;

        private void Start()
        {
            _currentHealthPoints = _startHealthPoints;
        }

        public void SetBrickIndex(int index)
        {
            _brickIndex = index;
        }

        public void SetInitWall(Wall wall)
        {
            _wall = wall;
        }

        public void ResetHealtPoints()
        {
            _currentHealthPoints = _startHealthPoints;
        }

        public void TakeDamage(int damage)
        {
            _currentHealthPoints -= damage;

            if (_currentHealthPoints <= 0 && gameObject.activeInHierarchy)
            {
                DestroyBrick();
            }
        }

        public void EjectionDuplicate()
        {
            Vector3 randomDirection = Random.insideUnitSphere;
            StartCoroutine(Ejection(randomDirection));
        }

        private void DestroyBrick()
        {
            _wall.BrickDestroy(this);
            gameObject.SetActive(false);
        }

        private IEnumerator Ejection(Vector3 direction)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _deactivationDelay)
            {
                transform.position += direction * _ejectionSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}