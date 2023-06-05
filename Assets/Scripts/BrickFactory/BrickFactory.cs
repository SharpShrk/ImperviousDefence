using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BricksStorage))]

public class BrickFactory : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private BricksStorage _bricksStorage;
    [SerializeField] private bool _hasResources = true;
    [SerializeField] private float _productionTime = 2.0f;

    private Vector3 _brickSize;
    private Coroutine _produceBrickCoroutine;

    public event Action<float> OnProductionUpdate;
    public float ProductionTime => _productionTime;

    private void Start()
    {
        _brickSize = _brickPrefab.GetComponent<Brick>().BrickSize;
        _produceBrickCoroutine = StartCoroutine(ProduceBricks());
    }

    private void OnEnable()
    {
        _bricksStorage.OnStorageFull += StopProduceBricks;
    }

    private void OnDisable()
    {
        _bricksStorage.OnStorageFull -= StopProduceBricks;
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

    private void StopProduceBricks()
    {
        if(_produceBrickCoroutine != null)
        {
            StopCoroutine(_produceBrickCoroutine);
        }
    }
}
