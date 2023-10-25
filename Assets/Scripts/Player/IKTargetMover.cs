using UnityEngine;

public class IKTargetMover : MonoBehaviour
{
    [SerializeField] private float _distanceFromPlayer = 2f;
    [SerializeField] private Transform _startPosition;

    private void Start()
    {
        transform.position = new Vector3(_startPosition.position.x + _distanceFromPlayer, _startPosition.position.y, _startPosition.position.z);
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, _startPosition.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldMousePosition = ray.GetPoint(distance);
            Vector3 toCursor = worldMousePosition - _startPosition.position;

            toCursor.y = 0;

            Vector3 targetPosition = _startPosition.position + toCursor.normalized * _distanceFromPlayer;

            targetPosition.y = _startPosition.position.y;
            transform.position = targetPosition;
        }
    }
}
