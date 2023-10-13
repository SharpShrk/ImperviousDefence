using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private int _defaultDamage = 10;
    
    private int _damage;
    private Rigidbody _rigidbody;
    private BulletPool _bulletPool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _damage = _defaultDamage;
    }

    private void OnEnable()
    {
        StartCoroutine(DisableBulletAfterTime());
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward;
        direction.y = 0;
        Vector3 movement = direction.normalized * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(transform.position + movement);
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

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public int GetDefaultDamage()
    {
        return _defaultDamage;
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
