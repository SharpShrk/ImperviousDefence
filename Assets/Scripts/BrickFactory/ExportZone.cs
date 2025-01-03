using UnityEngine;
using Walls;

namespace BrickFactories
{
    public class ExportZone : MonoBehaviour
    {
        [SerializeField] private Wall _wall;

        public WallRepair GetWallRepairComponent()
        {
            if (_wall.TryGetComponent(out WallRepair wallRepair))
            {
                return wallRepair;
            }

            return null;
        }
    }
}