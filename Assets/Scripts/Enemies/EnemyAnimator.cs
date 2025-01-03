using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        private const string IsDiedAnimatorTrigger = "isDied";

        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Die()
        {
            _animator.SetTrigger(IsDiedAnimatorTrigger);
        }
    }
}