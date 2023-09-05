using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private GameObject _rotatingPlatform;
    [SerializeField] private GameObject _gun;
    [SerializeField] private Transform _shootPoint;

    private BulletPool _bulletPool;
    private GameObject _target;
    private float _currentAttackCooldown;

    public bool IsPlaced { get; private set; }

    private void Start()
    {              
        IsPlaced = false;
        _currentAttackCooldown = _attackCooldown;
    }

    public void Init(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }
    public void PlaceTurret()
    {
        IsPlaced = true;  
        gameObject.SetActive(true);
        StartCoroutine(Attack());
    }

    public void SetAttackSpeed(float attackSpeedMultiplier)
    {
        _currentAttackCooldown = _attackCooldown * attackSpeedMultiplier;
    }

    private GameObject SearchAttackTarget()
    {
        _target = null;
        float closestDistance = _attackRange;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _attackRange);

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    _target = collider.gameObject;
                }
            }
        }

        return _target;
    }

    private void RotationGun(GameObject target)
    {
        float _rotationSpeed = 500f;

        Vector3 targetDirection = target.transform.position - _rotatingPlatform.transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion currentRotation = _rotatingPlatform.transform.rotation;
        Vector3 currentRotationEuler = currentRotation.eulerAngles;
        float yRotation = targetRotation.eulerAngles.y;
        Vector3 newRotationEuler = new Vector3(currentRotationEuler.x, yRotation, currentRotationEuler.z);
        Quaternion newTargetRotation = Quaternion.Euler(newRotationEuler);
        _rotatingPlatform.transform.rotation = Quaternion.Lerp(currentRotation, newTargetRotation, Time.deltaTime * _rotationSpeed);

        Vector3 directionToTarget = target.transform.position - _gun.transform.position;
        float yDifference = directionToTarget.y;
        float distanceToTarget = directionToTarget.magnitude;
        float angleToTarget = Mathf.Atan2(yDifference, distanceToTarget) * Mathf.Rad2Deg;
        angleToTarget = Mathf.Clamp(angleToTarget, -75f, 75f);

        _gun.transform.localRotation = Quaternion.Euler(-angleToTarget, 0, 0);
    }

    private IEnumerator Attack()
    {       
        while(true)
        {
            var attackCooldown = new WaitForSeconds(_currentAttackCooldown);
            GameObject target = SearchAttackTarget();

            if (target != null)
            {
                RotationGun(target);
                Shoot();
            }

            yield return attackCooldown;
        }
    }

    private void Shoot() //присвоить пуле урон
    {
        GameObject bullet = _bulletPool.GetBullet();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
    }
}
