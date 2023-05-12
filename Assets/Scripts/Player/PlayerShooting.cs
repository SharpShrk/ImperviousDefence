using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private float _fireRate = 0.2f;

    private PlayerInputHandler _inputHandler;
    private bool _canShoot = true;

    private void Start()
    {
        _inputHandler = new PlayerInputHandler(Camera.main);
        _inputHandler.Enable();

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

        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = _gunTransform.position;
        bullet.transform.rotation = _gunTransform.rotation;

        _canShoot = false;
        StartCoroutine(ShootingDelay());
    }

    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }
}
