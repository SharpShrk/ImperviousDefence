using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}