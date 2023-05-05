using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _wallBlockPrefab;
    [SerializeField] private int _wallWidth = 6;
    [SerializeField] private int _wallHeight = 10;
    [SerializeField] private float _blockWidth = 0.25f;
    [SerializeField] private float _blockHeight = 0.09f;
    [SerializeField] private float _horizontalSpacing = 0.0025f;
    [SerializeField] private float _verticalSpacing = 0.0025f;
    [SerializeField] private float _spawnDelay = 0.05f;
    [SerializeField] private float _spawnHeight = 1.5f;
    [SerializeField] private float _dropDuration = 0.05f;

    private List<GameObject> _wallBlocks = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(BuildWall());
    }

    private IEnumerator BuildWall()
    {
        int blockIndex = 0;

        for (int y = 0; y < _wallHeight; y++)
        {
            for (int x = 0; x < _wallWidth; x++)
            {
                GameObject block = Instantiate(_wallBlockPrefab, transform);

                float xPos = x * (_blockWidth + _horizontalSpacing);
                float yPos = y * (_blockHeight + _verticalSpacing);
                Vector3 startPosition = new Vector3(xPos, yPos + _spawnHeight, 0);
                block.transform.localPosition = startPosition;

                block.GetComponent<Brick>().SetBlockIndex(blockIndex);

                _wallBlocks.Add(block);

                blockIndex++;

                StartCoroutine(DropBlock(block, startPosition, new Vector3(xPos, yPos, 0), _dropDuration));

                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }

    private IEnumerator DropBlock(GameObject block, Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            block.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            yield return null;
        }

        block.transform.localPosition = endPosition;
    }
}