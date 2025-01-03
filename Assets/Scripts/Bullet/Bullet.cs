using Enemies;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float _speed = 10f;
        [SerializeField] protected float _lifetime = 3f;
        [SerializeField] protected int _defaultDamage = 10;

        protected int _damage;
        protected Rigidbody _rigidbody;
        protected WaitForSeconds _waitLifetime;

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _damage = _defaultDamage;
            _waitLifetime = new WaitForSeconds(_lifetime);
        }

        protected void OnEnable()
        {
            StartCoroutine(DisableBulletAfterTime());
        }

        protected void FixedUpdate()
        {
            Vector3 direction = transform.forward;
            Vector3 movement = direction.normalized * _speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(transform.position + movement);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }

            ReturnToPool();
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        protected IEnumerator DisableBulletAfterTime()
        {
            yield return _waitLifetime;
            ReturnToPool();
        }

        protected abstract void ReturnToPool();
    }
}