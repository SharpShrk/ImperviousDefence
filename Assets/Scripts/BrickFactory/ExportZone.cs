using UnityEngine;
using Walls;

namespace BrickFactory
{
    public class ExportZone : MonoBehaviour
    {
        [SerializeField] private Wall _wall;

        public WallRepair GetWallRepairComponent()
        {
            WallRepair wallRepair = _wall.GetComponent<WallRepair>();
            return wallRepair;
        }
    }
}