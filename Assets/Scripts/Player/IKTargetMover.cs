using UnityEngine;

public class IKTargetMover : MonoBehaviour
{
    [SerializeField] private float _distanceFromPlayer = 2f;
    [SerializeField] private Transform _startPosition;

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // ��������� ������� ��������� �������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, _startPosition.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldMousePosition = ray.GetPoint(distance);

            // ��������� ������� �� ��������� ������ �� �������
            Vector3 toCursor = worldMousePosition - _startPosition.position;

            // ������� ������������ ����������
            toCursor.y = 0;

            // ������������ � ���������������
            Vector3 targetPosition = _startPosition.position + toCursor.normalized * _distanceFromPlayer;

            // ��������� ������ � �������
            targetPosition.y = _startPosition.position.y;
            transform.position = targetPosition;
        }
    }
}
