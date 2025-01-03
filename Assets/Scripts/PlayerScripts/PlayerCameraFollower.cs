using UnityEngine;

public class PlayerCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lerpSpeed;

    private void LateUpdate()
    {
        Vector3 targetPosition = _playerTransform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpSpeed);
    }
}