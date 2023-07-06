using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    [SerializeField] private Vector3 _size = new Vector3();
    [SerializeField] private int _healthPoints;
    [SerializeField] private float _ejectionSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _deactivationDelay;

    public event Action<Brick> OnBrickDestroyed;

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

        if (_healthPoints <= 0 && gameObject.activeInHierarchy)
        {
            DestroyBrick();            
        }
    }

    private void DestroyBrick()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        StartCoroutine(Ejection(randomDirection));
        OnBrickDestroyed?.Invoke(this);
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
}
