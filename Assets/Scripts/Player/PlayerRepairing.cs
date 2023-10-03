using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRepairing : MonoBehaviour
{
    [SerializeField] private PlayerBricksBag _bricksBag;

    //private PlayerInputHandler _inputHandler;
    private WallRepair _currentWallRepair;

    /*private void OnEnable()
    {
        _inputHandler = new PlayerInputHandler(Camera.main);
        _inputHandler.Enable();

        _inputHandler.InputActions.Player.Use.performed += ctx => AttemptWallRepair();
    }*/

    /*private void OnDisable()
    {
        _inputHandler.Disable();
    }*/

    private void AttemptWallRepair()
    {
        if (_currentWallRepair == null) return;

        int neededBricks = _currentWallRepair.GetRequiredBrickCount();
        int availableBricks = _bricksBag.CurrentBrickCount;

        int repairBricks = Mathf.Min(neededBricks, availableBricks);

        if (_bricksBag.RemoveBricks(repairBricks))
        {
            _currentWallRepair.Repair(repairBricks, unusedBricks => {_bricksBag.AddBricks(unusedBricks);});
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ExportZone>(out ExportZone exportZone))
        {
            _currentWallRepair = exportZone.GetWallRepairComponent();
            AttemptWallRepair();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ExportZone>())
        {
            _currentWallRepair = null;
        }
    }
}