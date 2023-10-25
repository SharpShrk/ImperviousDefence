using UnityEngine;

public class ExportZone : MonoBehaviour
{
    [SerializeField] private Wall _wall;

    public WallRepair GetWallRepairComponent()
    {
        WallRepair wallRepair = _wall.GetComponent<WallRepair>();
        return wallRepair;
    }
}
