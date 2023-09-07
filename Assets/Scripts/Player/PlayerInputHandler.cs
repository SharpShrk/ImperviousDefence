using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler
{
    public InputSystem InputActions;

    private Vector2 _moveInput;
    private Camera _mainCamera;
    private string _groundLabel = "Ground";

    public PlayerInputHandler(Camera mainCamera)
    {
        InputActions = new InputSystem();
        _mainCamera = mainCamera;

        InputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        InputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    public void Enable()
    {
        InputActions.Enable();
    }

    public void Disable()
    {
        InputActions.Disable();
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
        Vector2 screenPosition = InputActions.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask(_groundLabel)))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}