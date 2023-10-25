using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    public List<Transform> AttackPoints = new();
    
    private BrickPool _brickPool;
    private List<GameObject> _wallBlocks = new List<GameObject>();

    public event UnityAction WallDestroed;

    public List<GameObject> DestroyedBricks { get; private set; } = new List<GameObject>();

    public int RequiredBrickCount => _wallBlocks.Count;

    private void Awake()
    {
        _brickPool = FindObjectOfType<BrickPool>();
    }

    public void SetBricks(List<GameObject> bricks)
    {
        _wallBlocks = bricks;
    }

    public void TakeDamage(int damage)
    {
        if (_wallBlocks.Count > 0)
        {
            GameObject maxIndexBlock = GetMaxIndexBrick();

            if (maxIndexBlock.activeInHierarchy)
            {
                maxIndexBlock.GetComponent<Brick>().TakeDamage(damage);                
            }
        }
        else
        {
            WallDestroed?.Invoke();
        }
    }

    public void SetRepairedBrick(GameObject brick)
    {
        _wallBlocks.Add(brick);
        brick.GetComponent<Brick>().ResetHealtPoints();
    }

    public void BrickDestroy(Brick brick)
    {
        _wallBlocks.Remove(brick.gameObject);
        DestroyedBricks.Add(brick.gameObject);

        EjectionDuplicateBrick(brick);
    }

    private GameObject GetMaxIndexBrick()
    {
        GameObject maxIndexBlock = _wallBlocks[0];

        foreach (GameObject block in _wallBlocks)
        {
            if (block.GetComponent<Brick>().BrickIndex > maxIndexBlock.GetComponent<Brick>().BrickIndex)
            {
                maxIndexBlock = block;
            }
        }

        return maxIndexBlock;
    }   

    private void EjectionDuplicateBrick(Brick brick)
    {        
        GameObject duplicateBrick = _brickPool.GetBrick();
        duplicateBrick.transform.position = brick.gameObject.transform.position;
        duplicateBrick.GetComponent<Brick>().EjectionDuplicate();
    }
}
