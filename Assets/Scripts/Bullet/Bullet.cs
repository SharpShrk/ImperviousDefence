using Enemies;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float Speed = 10f;
        [SerializeField] protected float Lifetime = 3f;
        [SerializeField] protected int DefaultDamage = 10;

        protected int Damage;
        protected Rigidbody Rigidbody;
        protected WaitForSeconds WaitLifetime;

        protected void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Damage = DefaultDamage;
            WaitLifetime = new WaitForSeconds(Lifetime);
        }

        protected void OnEnable()
        {
            StartCoroutine(DisableBulletAfterTime());
        }

        protected void FixedUpdate()
        {
            Vector3 direction = transform.forward;
            Vector3 movement = direction.normalized * Speed * Time.fixedDeltaTime;
            Rigidbody.MovePosition(transform.position + movement);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
            }

            ReturnToPool();
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        protected IEnumerator DisableBulletAfterTime()
        {
            yield return WaitLifetime;
            ReturnToPool();
        }

        protected abstract void ReturnToPool();
    }
}