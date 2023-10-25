using UnityEngine;
using UnityEngine.Events;

public class ButtonControlProdaction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteButton;

    public event UnityAction ButtonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() && other is SphereCollider)
        {
            OnButtonPressed();
        }
    }

    private void OnButtonPressed()
    {
        ButtonPressed?.Invoke();
    }
}
