using Bullets;
using PlayerScripts;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Mobile
{
    public class MobilePlayerShooting : MonoBehaviour
    {
        private const string AnimatorTriggerShoot = "Shoot";

        [SerializeField] private Transform _gunTransform;
        [SerializeField] private CameraShake _cameraShake;
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private BulletPlayerPool _bulletPool;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RightJoystick _rightJoystick;
        [SerializeField] private float _inputThreshold = 0.3f;

        private Animator _animator;
        private bool _canShoot = true;
        private Coroutine _shootingCoroutine;
        private Player _player;
        private WaitForSeconds _waitFireRate;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _player = GetComponent<Player>();
            _waitFireRate = new WaitForSeconds(_fireRate);
        }

        private void FixedUpdate()
        {
            Vector3 joystickInput = _rightJoystick.GetInputDirection();

            if (joystickInput.magnitude >= _inputThreshold && _shootingCoroutine == null)
            {
                StartShooting();
            }
            else if (joystickInput.magnitude < _inputThreshold && _shootingCoroutine != null)
            {
                StopShooting();
            }
        }

        private void StartShooting()
        {
            if (_shootingCoroutine == null)
            {
                _shootingCoroutine = StartCoroutine(ShootingRoutine());
            }
        }

        private void StopShooting()
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }

        private IEnumerator ShootingRoutine()
        {
            while (_player.enabled)
            {
                if (_canShoot && Time.timeScale != 0)
                {
                    Shoot();
                }

                yield return _waitFireRate;
            }
        }

        private void Shoot()
        {
            BulletPlayer bullet = _bulletPool.GetBullet();
            bullet.transform.position = _gunTransform.position;
            bullet.transform.rotation = _gunTransform.rotation;

            _animator.SetLayerWeight(1, 1);
            _animator.SetTrigger(AnimatorTriggerShoot);
            _cameraShake.TryStartShake();
            _audioSource.Play();

            _canShoot = false;
            StartCoroutine(ShootingDelay());
        }

        private IEnumerator ShootingDelay()
        {
            yield return _waitFireRate;
            _animator.SetLayerWeight(1, 0);
            _canShoot = true;
        }
    }
}