using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Vector3 _size = new Vector3(0.5f, 0.18f, 0.20f);
    [SerializeField] private int _healthPoints = 2;
    [SerializeField] private float _ejectionSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 360f;
    [SerializeField] private float _deactivationDelay = 0.7f;

    public int BlockIndex { get; private set; }
    public Vector3 InitialPosition { get; private set; }
    public Vector3 BrickSize => _size;

    public void SetBlockIndex(int index)
    {
        BlockIndex = index;
    }

    public void SetInitialPosition(Vector3 position)
    {
        InitialPosition = position;
    }

    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            DestroyBrick();
        }
    }

    private void Eject()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        StartCoroutine(Ejection(randomDirection));
    }

    private IEnumerator Ejection(Vector3 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _deactivationDelay)
        {
            transform.position += direction * _ejectionSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void DestroyBrick()
    {
        Eject();
    }
}
