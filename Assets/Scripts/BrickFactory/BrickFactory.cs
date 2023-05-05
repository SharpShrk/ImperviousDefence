using System;
using System.Collections;
using UnityEngine;

public class BrickFactory : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private BricksStorage _bricksStorage;
    [SerializeField] private bool _hasResources = true;
    [SerializeField] private float _productionTime = 2.0f;

    private Vector3 _brickSize;

    public event Action<float> OnProductionUpdate;
    public float ProductionTime => _productionTime;

    private void Start()
    {
        _brickSize = _brickPrefab.GetComponent<Brick>().BrickSize;
        StartCoroutine(ProduceBricks());
    }

    private IEnumerator ProduceBricks()
    {
        while (_hasResources)
        {
            float elapsedTime = 0f;
            while (elapsedTime < _productionTime)
            {
                elapsedTime += Time.deltaTime;
                OnProductionUpdate?.Invoke(elapsedTime / _productionTime);
                yield return null;
            }
            _bricksStorage.AddBrick(_brickPrefab, _brickSize);
        }
    }
}
