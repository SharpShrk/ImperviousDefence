using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Die()
        {
            _animator.SetTrigger("isDied");
        }
    }
}
