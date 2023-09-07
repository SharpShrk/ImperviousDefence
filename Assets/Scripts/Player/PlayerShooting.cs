using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private float _fireRate = 0.2f;
    [SerializeField] private BulletPool _bulletPool;

    private PlayerInputHandler _inputHandler;
    private Animator _animator;
    private bool _canShoot = true;

    private void Start()
    {
        _inputHandler = new PlayerInputHandler(Camera.main);
        _inputHandler.Enable();
        _animator = GetComponent<Animator>();

        _inputHandler.InputActions.Player.Shoot.performed += ctx => Shoot();
    }

    private void OnDestroy()
    {
        _inputHandler.Disable();
    }

    private void Shoot()
    {
        if (_canShoot == false)
        {
            return;
        }

        GameObject bullet = _bulletPool.GetBullet();
        bullet.transform.position = _gunTransform.position;
        bullet.transform.rotation = _gunTransform.rotation;
        _animator.SetLayerWeight(1, 1);
        _animator.SetTrigger("Shoot");

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
