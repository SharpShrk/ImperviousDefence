using System.Collections;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Transform _gateTransform;
    [SerializeField] private Vector3 _loweredPositionOffset;
    [SerializeField] private float _movementTime;
    
    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _initialPosition = _gateTransform.position;
        _targetPosition = _initialPosition + _loweredPositionOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(MoveGate(_gateTransform.position, _targetPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(MoveGate(_gateTransform.position, _initialPosition));
        }
    }

    private IEnumerator MoveGate(Vector3 startPosition, Vector3 endPosition)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _movementTime)
        {
            _gateTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _movementTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _gateTransform.position = endPosition;
    }
}
