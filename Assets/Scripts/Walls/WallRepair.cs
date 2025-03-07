using System;
using System.Collections;
using UnityEngine;

namespace Walls
{
    [RequireComponent(typeof(Wall))]

    public class WallRepair : MonoBehaviour
    {
        [SerializeField] private float _repairDelay;
        [SerializeField] private float _speedRepairBrick;

        private Wall _wall;
        private bool _isRepairing = false;

        private void Awake()
        {
            _wall = GetComponent<Wall>();
        }

        public int GetRequiredBrickCount()
        {
            return _wall.RequiredBrickCount;
        }

        public void Repair(int countBricks, Action<int> onRepairComplete)
        {
            if (!_isRepairing)
            {
                StartCoroutine(RepairCoroutine(countBricks, onRepairComplete));
            }
        }

        public IEnumerator RepairCoroutine(int countBricks, Action<int> onRepairComplete)
        {
            _isRepairing = true;
            int unusedBricks = 0;

            for (int i = 0; i < countBricks; i++)
            {
                if (_wall.DestroyedBricks.Count == 0)
                {
                    unusedBricks = countBricks - i;
                    break;
                }

                GameObject destroyedBrick = _wall.DestroyedBricks[_wall.DestroyedBricks.Count - 1];
                _wall.DestroyedBricks.RemoveAt(_wall.DestroyedBricks.Count - 1);
                _wall.SetRepairedBrick(destroyedBrick);

                destroyedBrick.SetActive(true);

                StartCoroutine(ReturnBrick(destroyedBrick));

                var repairDelay = new WaitForSeconds(_repairDelay);

                AudioSource audioSource = destroyedBrick.GetComponent<AudioSource>();
                audioSource.Play();

                yield return repairDelay;
            }

            _isRepairing = false;

            onRepairComplete?.Invoke(unusedBricks);
        }

        private IEnumerator ReturnBrick(GameObject brick)
        {
            float elapsedTime = 0.0f;

            Vector3 spawnHeight = new(0, 0.5f, 0);
            Vector3 startPosition = brick.transform.position + spawnHeight;
            Vector3 endPosition = brick.transform.position;

            brick.transform.position = startPosition;

            while (elapsedTime < _speedRepairBrick)
            {
                elapsedTime += Time.deltaTime;
                brick.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _speedRepairBrick);
                yield return null;
            }

            brick.transform.position = endPosition;
        }
    }
}