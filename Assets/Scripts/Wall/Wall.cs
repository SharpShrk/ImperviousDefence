using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform[] AttackPoints;

    private List<GameObject> _wallBlocks = new List<GameObject>();

    public void SetBlocks(List<GameObject> blocks)
    {
        _wallBlocks = blocks;
    }

    public void TakeDamage(int damage)
    {
        if (_wallBlocks.Count > 0)
        {
            GameObject maxIndexBlock = _wallBlocks[0];

            foreach (GameObject block in _wallBlocks)
            {
                if (block.GetComponent<Brick>().BlockIndex > maxIndexBlock.GetComponent<Brick>().BlockIndex)
                {
                    maxIndexBlock = block;
                }
            }

            if(maxIndexBlock.activeInHierarchy)
            {
            maxIndexBlock.GetComponent<Brick>().TakeDamage(damage);
            }

            if (!maxIndexBlock.activeSelf) //не забыть сделать добавление в список при ремонте блока
            {
                _wallBlocks.Remove(maxIndexBlock);
            }
        }
    }

}
