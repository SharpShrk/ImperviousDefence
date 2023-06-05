using UnityEngine;

public class TurretFactory : MonoBehaviour
{
    [SerializeField] private BulletPool turretBulletPool;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private Transform[] _spawnTurretPonts;
    [SerializeField] private Transform _turretContainer;

    private void Start()
    {
        CreateTurret();
    }

    private void CreateTurret()
    {
        for(int i = 0; i < _spawnTurretPonts.Length; i++)
        {
            GameObject turretObject = Instantiate(turretPrefab, _spawnTurretPonts[i].position, Quaternion.identity);
            turretObject.transform.SetParent(_turretContainer.transform);
            Turret turret = turretObject.GetComponent<Turret>();
            turret.Init(turretBulletPool);
        }        
    }
}
