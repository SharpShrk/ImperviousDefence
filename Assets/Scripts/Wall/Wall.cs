using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform[] AttackPoints; //убрать паблик сделать геттер

    private List<GameObject> _wallBlocks = new List<GameObject>();

    private void OnDisable()
    {
        foreach (GameObject brickObject in _wallBlocks)
        {
            Brick brick = brickObject.GetComponent<Brick>();
            brick.OnBrickDestroyed -= HandleBrickDestroyed;
        }
    }

    public void SetBricks(List<GameObject> bricks)
    {
        _wallBlocks = bricks;

        foreach (GameObject brickObject in _wallBlocks)
        {
            Brick brick = brickObject.GetComponent<Brick>();
            brick.OnBrickDestroyed += HandleBrickDestroyed;
        }
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
    }

    private GameObject GetMaxIndexBrick()
    {
        GameObject maxIndexBlock = _wallBlocks[0];

        foreach (GameObject block in _wallBlocks)
        {
            if (block.GetComponent<Brick>().BlockIndex > maxIndexBlock.GetComponent<Brick>().BlockIndex)
            {
                maxIndexBlock = block;
            }
        }

        return maxIndexBlock;
    }

    private void HandleBrickDestroyed(Brick brick)
    {
        _wallBlocks.Remove(brick.gameObject);
    }
}
