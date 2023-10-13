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
        // Получение мировых координат курсора
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, _startPosition.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldMousePosition = ray.GetPoint(distance);

            // Получение вектора от заданного центра до курсора
            Vector3 toCursor = worldMousePosition - _startPosition.position;

            // Убираем вертикальную компоненту
            toCursor.y = 0;

            // Нормализация и масштабирование
            Vector3 targetPosition = _startPosition.position + toCursor.normalized * _distanceFromPlayer;

            // Установка высоты и позиции
            targetPosition.y = _startPosition.position.y;
            transform.position = targetPosition;
        }
    }
}
