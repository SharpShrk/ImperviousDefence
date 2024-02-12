using UnityEngine;

namespace BrickFactories
{
    public class AnimationBrickFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _factoryObject;

        private Animator _animator;

        private void Start()
        {
            _animator = _factoryObject.GetComponent<Animator>();
            _animator.speed = 0;
        }

        public void PlayAnimation()
        {
            _animator.speed = 1;
        }

        public void StopAnimation()
        {
            _animator.speed = 0;
        }
    }
}