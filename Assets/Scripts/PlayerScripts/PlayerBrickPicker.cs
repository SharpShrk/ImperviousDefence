using BrickFactories;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerBrickPicker : MonoBehaviour
    {
        [SerializeField] private BricksStorage _bricksStorage;
        [SerializeField] private PlayerBricksBag _brickBag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ImportZone>() != null)
            {
                PickBrick();
            }
        }

        private void PickBrick()
        {
            if (_bricksStorage.BrickCount == 0)
            {
                return;
            }

            int bricksNeeded = _brickBag.AvailableCapacity;

            int actualBricksToPick = Mathf.Min(_bricksStorage.BrickCount, bricksNeeded);

            if (actualBricksToPick == 0)
            {
                return;
            }

            _bricksStorage.RemoveBricks(actualBricksToPick);

            if (!_brickBag.AddBricks(actualBricksToPick))
            {
                return;
            }
        }
    }
}