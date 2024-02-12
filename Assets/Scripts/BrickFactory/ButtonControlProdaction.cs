using PlayerScripts;
using System;
using UnityEngine;

namespace BrickFactories
{
    public class ButtonControlProdaction : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteButton;

        public event Action ButtonPressed;

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
}