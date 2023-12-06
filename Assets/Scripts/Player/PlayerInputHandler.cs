using UnityEngine;

public class PlayerInputHandler
{
    private InputSystem _inputActions;
    private Vector2 _moveInput;
    private Camera _mainCamera;
    private string _groundLabel = "Ground";

    public InputSystem InputActions => _inputActions;

    public PlayerInputHandler(Camera mainCamera)
    {
        _inputActions = new InputSystem();
        _mainCamera = mainCamera;

        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    public void Enable()
    {
        _inputActions.Enable();
    }

    public void Disable()
    {
        _inputActions.Disable();
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(_moveInput.x, 0, _moveInput.y);
    }

    public Vector3 GetLookDirection(Vector3 playerPosition)
    {
        return GetMouseWorldPosition() - playerPosition;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector2 screenPosition = _inputActions.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask(_groundLabel)))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}