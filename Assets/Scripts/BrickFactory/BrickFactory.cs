using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Walls;

namespace BrickFactory
{
    public class BrickFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _brickPrefab;
        [SerializeField] private BricksStorage _bricksStorage;
        [SerializeField] private bool _hasResources = true;
        [SerializeField] private float _productionTime;
        [SerializeField] private GameObject _factoryObject;
        [SerializeField] private AudioSource _audioSource;

        private Animator _animator;
        private Vector3 _brickSize;
        private Queue<int> _productionQueue = new Queue<int>();

        public float ProductionTime => _productionTime;

        private void Start()
        {
            _brickSize = _brickPrefab.GetComponent<Brick>().BrickSize;
            _animator = _factoryObject.GetComponent<Animator>();
            _animator.speed = 0;
            StartCoroutine(ProduceBricks());
        }

        public void AddToProductionQueue(int amount)
        {
            _productionQueue.Enqueue(amount);
        }

        private IEnumerator ProduceBricks()
        {
            while (_hasResources)
            {
                if (_productionQueue.Count > 0 && _bricksStorage.IsStorageFull == false)
                {
                    int bricksToProduce = _productionQueue.Dequeue();
                    _animator.speed = 1;
                    _audioSource.Play();

                    for (int i = 0; i < bricksToProduce; i++)
                    {
                        if (_bricksStorage.IsStorageFull)
                        {
                            _productionQueue.Enqueue(bricksToProduce - i + 1);
                            break;
                        }

                        float elapsedTime = 0f;

                        while (elapsedTime < _productionTime)
                        {
                            elapsedTime += Time.deltaTime;
                            yield return null;
                        }

                        _bricksStorage.AddBrick(_brickPrefab, _brickSize);
                    }
                }

                _animator.speed = 0;
                _audioSource.Stop();

                yield return null;
            }
        }
    }
}