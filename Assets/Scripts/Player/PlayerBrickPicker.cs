using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrickPicker : MonoBehaviour
{
    [SerializeField] private BricksStorage _bricksStorage;
    [SerializeField] private PlayerBricksBag _brickBag;

    //private bool _isInTriggerZone = false;
    //private PlayerInputHandler _inputHandler;

    private void Start()
    {
        /*_inputHandler = new PlayerInputHandler(Camera.main);
        _inputHandler.Enable();

        _inputHandler.InputActions.Player.Use.performed += ctx => PickBrick();*/
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<ImportZone>() != null)
        {
            PickBrick();
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BricksStorage>() != null)
        {
            _isInTriggerZone = false;
            Debug.Log("�������: ��� ���� ������");
        }
    }*/

    private void PickBrick()
    {
        /*if (_isInTriggerZone == false)
        {
            Debug.Log("������ �� ������ � ���������");
            return;
        }*/

        if (_bricksStorage.BrickCount == 0)
        {
            Debug.Log("������������ �������� � ���������!");
            return;
        }

        int bricksNeeded = _brickBag.AvailableCapacity;

        int actualBricksToPick = Mathf.Min(_bricksStorage.BrickCount, bricksNeeded);

        if (actualBricksToPick == 0)
        {
            Debug.Log("������������ �������� � ��������� ��� ����� �����!");
            return;
        }

        _bricksStorage.RemoveBricks(actualBricksToPick);

        if (!_brickBag.AddBricks(actualBricksToPick))
        {
            Debug.Log("������ ��� ���������� ������� � �����");
            return;
        }

        Debug.Log("������� �����");
    }
}