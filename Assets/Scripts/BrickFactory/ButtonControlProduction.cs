using PlayerScripts;
using System;
using UnityEngine;

namespace BrickFactories
{
    public class ButtonControlProduction : MonoBehaviour
    {
        public event Action ButtonPressed;

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            var playerComponent = other.GetComponent<Player>();

            if (playerComponent != null && other is SphereCollider)
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