using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonControlProdaction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteButton;

    private bool _isProductionActivate;
    private Color _productionColor = Color.green;
    private Color _startColor;

    public event UnityAction ButtonPressed;

    private void OnEnable()
    {
        _isProductionActivate = false;
        _startColor = _spriteButton.color;
        //подписка на фактори, чтобы менять цвет кнопки, если производство запущено
    }

    private void OnDisable()
    {
        //отписка от фактори
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() && other is BoxCollider)
        {
            OnButtonPressed();
        }
    }

    private void OnButtonPressed()
    {
        ButtonPressed?.Invoke();
    }

    private void SetProductionColor()
    {
        _spriteButton.color = _productionColor;
    }

    private void SetStartColor()
    {
        _spriteButton.color = _startColor;
    }
}
