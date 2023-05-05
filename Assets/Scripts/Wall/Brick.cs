using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Vector3 _size = new Vector3(0.5f, 0.18f, 0.20f);

    public int BlockIndex { get; private set; }

    public Vector3 BrickSize => _size;

    public void SetBlockIndex(int index)
    {
        BlockIndex = index;
    }
}
