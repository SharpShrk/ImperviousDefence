using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = gameObject.GetComponent<Animation>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _animation.Play("Open");
            Debug.Log("открыть дверь");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _animation.Play("Close");
            Debug.Log("Закрыть дверь");
        }
    }
}
