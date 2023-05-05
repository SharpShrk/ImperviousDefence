using UnityEngine;

public class BricksStorage : MonoBehaviour
{
    [SerializeField] private Transform _brickSlotsParent;
    [SerializeField] private float _gapBetweenBricks = 0.005f;

    private Vector3 _brickSize;
    private int _brickCountInLayer = 9;
    private int _layersCount = 4;
    private int _currentBrickIndex = 0;

    public void AddBrick(GameObject brickPrefab, Vector3 brickSize)
    {
        _brickSize = brickSize;

        if (_currentBrickIndex >= _brickCountInLayer * _layersCount)
        {
            Debug.Log("Склад заполнен");
            return;
        }

        int layerIndex = _currentBrickIndex / _brickCountInLayer;
        int positionIndex = _currentBrickIndex % _brickCountInLayer;

        Vector3 brickPosition = CalculateBrickPosition(positionIndex, layerIndex);
        GameObject newBrick = Instantiate(brickPrefab, _brickSlotsParent);
        newBrick.transform.localPosition = brickPosition;
        newBrick.SetActive(true);
        newBrick.GetComponent<Brick>().SetBlockIndex(_currentBrickIndex);

        _currentBrickIndex++;
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
