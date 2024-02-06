using UnityEngine;

namespace Mobile
{
    public class MobileInputHandler
    {
        private LeftJoystick _moveJoystick;
        private RightJoystick _lookJoystick;

        public MobileInputHandler(LeftJoystick moveJoystick, RightJoystick lookJoystick)
        {
            _moveJoystick = moveJoystick;
            _lookJoystick = lookJoystick;
        }

        public Vector3 GetMoveDirection()
        {
            return _moveJoystick.GetInputDirection();
        }

        public Vector3 GetLookDirection(Vector3 currentPosition)
        {
            Vector3 lookDirection = _lookJoystick.GetInputDirection();
            return currentPosition + lookDirection;
        }
    }
}