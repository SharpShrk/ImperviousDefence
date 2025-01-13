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

        public void Initialize(BrickFactoryConfiguration configuration)
        {
            _configuration = configuration;
            StartCoroutine(ProduceBricks());
        }

        public void AddToProductionQueue(int amount)
        {
            _productionQueue.Enqueue(amount);
        }

        private IEnumerator ProduceBricks()
        {
            while (_gameOverChecker.IsGameOver == false)
            {
                if (_productionQueue.Count > 0 && _configuration.BricksStorage.IsStorageFull == false)
                {
                    int bricksToProduce = _productionQueue.Dequeue();

                    gameObject.GetComponent<AnimationBrickFactory>().PlayAnimation();
                    gameObject.GetComponent<AudioBrickFactory>().PlayAudio();

                    for (int i = 0; i < bricksToProduce; i++)
                    {
                        if (_configuration.BricksStorage.IsStorageFull)
                        {
                            _productionQueue.Enqueue(bricksToProduce - i + 1);
                            break;
                        }

                        float elapsedTime = 0f;

                        while (elapsedTime < _configuration.ProductionTime)
                        {
                            elapsedTime += Time.deltaTime;
                            yield return null;
                        }

                        _configuration.BricksStorage.AddBrick(_configuration.BrickPrefab, _configuration.BrickSize);
                    }
                }

                gameObject.GetComponent<AnimationBrickFactory>().StopAnimation();
                gameObject.GetComponent<AudioBrickFactory>().StopAudio();

                yield return null;
            }
        }
    }
}