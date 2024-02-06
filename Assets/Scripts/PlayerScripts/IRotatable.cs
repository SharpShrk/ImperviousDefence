using UnityEngine;

namespace PlayerScripts
{
    public interface IRotatable
    {
        void Rotate(Vector3 targetPosition);
    }
}