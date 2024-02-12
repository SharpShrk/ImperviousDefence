using UnityEngine;
using Walls;

namespace BrickFactories
{
    public class BrickFactoryConfiguration : MonoBehaviour
    {
        [SerializeField] private GameObject _brickPrefab;
        [SerializeField] private BricksStorage _bricksStorage;
        [SerializeField] private float _productionTime;

        private BrickFactory _brickFactory;
        private Vector3 _brickSize;

        public GameObject BrickPrefab => _brickPrefab;
        public BricksStorage BricksStorage => _bricksStorage;
        public float ProductionTime => _productionTime;
        public Vector3 BrickSize => _brickSize;

        private void Start()
        {
            _brickFactory = GetComponent<BrickFactory>();
            _brickSize = _brickPrefab.GetComponent<Brick>().BrickSize;
            _brickFactory.Initialize(this);
        }
    }
}
