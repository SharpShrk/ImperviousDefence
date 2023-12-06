using System.Collections.Generic;
using UnityEngine;

public class BricksStorage : MonoBehaviour
{
    [SerializeField] private Transform _brickSlotsParent;
    [SerializeField] private float _gapBetweenBricks = 0.005f;

    private Vector3 _brickSize;
    private int _brickCountInLayer = 9;
    private int _layersCount = 4;
    private int _currentBrickIndex = 0;
    private bool _isStorageFull = false;
    private List<GameObject> _bricks = new List<GameObject>();

    public int BrickCount { get => _currentBrickIndex; }

    public bool IsStorageFull => _isStorageFull;

    public void AddBrick(GameObject brickPrefab, Vector3 brickSize)
    {
        _brickSize = brickSize;

        if (_currentBrickIndex >= _brickCountInLayer * _layersCount)
        {
            _isStorageFull = true;
            return;
        }

        int layerIndex = _currentBrickIndex / _brickCountInLayer;
        int positionIndex = _currentBrickIndex % _brickCountInLayer;

        Vector3 brickPosition = CalculateBrickPosition(positionIndex, layerIndex);
        GameObject newBrick = Instantiate(brickPrefab, _brickSlotsParent);
        newBrick.transform.localPosition = brickPosition;
        newBrick.SetActive(true);
        _bricks.Add(newBrick);
        newBrick.GetComponent<Brick>().SetBrickIndex(_currentBrickIndex);

        _currentBrickIndex++;
    }

    public void RemoveBricks(int count)
    {
        if (_currentBrickIndex <= 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (_currentBrickIndex <= 0)
            {
                break;
            }

            GameObject brickToRemove = _bricks[_currentBrickIndex - 1];
            _bricks.Remove(brickToRemove);
            Destroy(brickToRemove);
            _currentBrickIndex--;
        }

        _isStorageFull = false;
    }

    private Vector3 CalculateBrickPosition(int positionIndex, int layerIndex)
    {
        int row = positionIndex / 3;
        int col = positionIndex % 3;

        float x = col * (_brickSize.x + _gapBetweenBricks);
        float y = layerIndex * (_brickSize.y + _gapBetweenBricks);
        float z = row * (_brickSize.z + _gapBetweenBricks);

        return new Vector3(x, y, z);
    }
}