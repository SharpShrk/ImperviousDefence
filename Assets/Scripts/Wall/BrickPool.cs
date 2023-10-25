using System.Collections.Generic;
using UnityEngine;

public class BrickPool : MonoBehaviour
{
    [SerializeField] private int _initialPoolSize = 15;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _parent;

    private Queue<GameObject> _brickPool;

    private void Awake()
    {
        InitializePool();
    }

    public GameObject GetBrick()
    {
        if (_brickPool.Count > 0)
        {
            GameObject brick = _brickPool.Dequeue();
            brick.SetActive(true);
            brick.transform.SetParent(_parent.transform);
            return brick;
        }

        GameObject newBrick = Instantiate(_prefab);
        return newBrick;
    }

    public void ReturnBrick(GameObject brick)
    {
        brick.SetActive(false);
        _brickPool.Enqueue(brick);
    }

    private void InitializePool()
    {
        _brickPool = new Queue<GameObject>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject brick = Instantiate(_prefab);
            brick.SetActive(false);
            brick.transform.SetParent(_parent.transform);
            _brickPool.Enqueue(brick);
        }
    }
}
