using UnityEngine;

namespace Walls
{
    public class WallContainer : MonoBehaviour
    {
        [SerializeField] private Wall[] _walls;

        public Wall[] Walls => _walls;
    }
}