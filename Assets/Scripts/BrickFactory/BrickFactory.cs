using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterface;

namespace BrickFactories
{
    [RequireComponent(typeof(AnimationBrickFactory))]
    [RequireComponent(typeof(AudioBrickFactory))]
    public class BrickFactory : MonoBehaviour
    {
        [SerializeField] private GameOverChecker _gameOverChecker;

        private BrickFactoryConfiguration _configuration;
        private Queue<int> _productionQueue = new Queue<int>();
        private AnimationBrickFactory _animationFactory;
        private AudioBrickFactory _audioFactory;

        public void Initialize(BrickFactoryConfiguration configuration)
        {
            _configuration = configuration;
            _animationFactory = GetComponent<AnimationBrickFactory>();
            _audioFactory = GetComponent<AudioBrickFactory>();
            StartCoroutine(ProductionRoutine());
        }

        public void AddToProductionQueue(int amount)
        {
            _productionQueue.Enqueue(amount);
        }

        private IEnumerator ProductionRoutine()
        {
            while (!_gameOverChecker.IsGameOver)
            {
                if (CanProduce())
                {
                    yield return StartCoroutine(ProcessProduction());
                }
                else
                {
                    yield return null;
                }
            }
        }

        private bool CanProduce()
        {
            return _productionQueue.Count > 0 && !_configuration.BricksStorage.IsStorageFull;
        }

        private IEnumerator ProcessProduction()
        {
            int bricksToProduce = _productionQueue.Dequeue();
            _animationFactory.PlayAnimation();
            _audioFactory.PlayAudio();

            for (int i = 0; i < bricksToProduce; i++)
            {
                if (_configuration.BricksStorage.IsStorageFull)
                {
                    _productionQueue.Enqueue(bricksToProduce - i);
                    break;
                }

                yield return ProduceSingleBrick();
            }

            _animationFactory.StopAnimation();
            _audioFactory.StopAudio();
        }

        private IEnumerator ProduceSingleBrick()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _configuration.ProductionTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _configuration.BricksStorage.AddBrick(_configuration.BrickPrefab, _configuration.BrickSize);
        }
    }
}