using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _fireRate = 0.2f;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private AudioSource _audioSource;

    private PlayerInputHandler _inputHandler;
    private Animator _animator;
    private bool _canShoot = true;
    private Coroutine _shootingCoroutine;

    private void Start()
    {
        _inputHandler = new PlayerInputHandler(Camera.main);
        _inputHandler.Enable();
        _animator = GetComponent<Animator>();

        _inputHandler.InputActions.Player.Shoot.performed += ctx => StartShooting();
        _inputHandler.InputActions.Player.Shoot.canceled += ctx => StopShooting();
    }

    private void OnDestroy()
    {
        _inputHandler.Disable();
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
        while (true)
        {
            if (_canShoot && Time.timeScale != 0)
            {
                Shoot();
            }

            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void Shoot()
    {
        GameObject bullet = _bulletPool.GetBullet();
        bullet.transform.position = _gunTransform.position;
        bullet.transform.rotation = _gunTransform.rotation;
        _animator.SetLayerWeight(1, 1);
        _animator.SetTrigger("Shoot");
        _cameraShake.Shake();
        _audioSource.Play();

        _canShoot = false;
        StartCoroutine(ShootingDelay());
    }

    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(_fireRate);
        _animator.SetLayerWeight(1, 0);
        _canShoot = true;
    }
}
