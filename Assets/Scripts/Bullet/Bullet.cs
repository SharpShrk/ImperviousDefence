using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private int _damage = 10;

    private Rigidbody _rigidbody;
    private BulletPool _bulletPool;

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
        var damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        ReturnToPool();
    }

    public void Init(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    private IEnumerator DisableBulletAfterTime()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        _bulletPool.ReturnBullet(gameObject);
    }
}
