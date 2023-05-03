using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 3f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableBulletAfterTime());
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }

    private IEnumerator DisableBulletAfterTime()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}
