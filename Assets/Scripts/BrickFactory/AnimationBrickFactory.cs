using UnityEngine;

namespace BrickFactories
{
    public class AnimationBrickFactory : MonoBehaviour
    {
        private const int StartAnimationSpeed = 1;
        private const int StopAnimationSpeed = 0;

        [SerializeField] private GameObject _factoryObject;

        private Animator _animator;

        private void Start()
        {
            _animator = _factoryObject.GetComponent<Animator>();
            _animator.speed = StopAnimationSpeed;
        }

        public void PlayAnimation()
        {
            _animator.speed = StartAnimationSpeed;
        }

        public void StopAnimation()
        {
            _animator.speed = StopAnimationSpeed;
        }
    }
}