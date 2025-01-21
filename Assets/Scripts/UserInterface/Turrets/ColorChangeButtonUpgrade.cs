using PlayerScripts;
using System;
using System.Collections;
using UnityEngine;

namespace TurretsUI
{
    public class ColorChangeButtonUpgrade : MonoBehaviour
    {
        [SerializeField] private GameObject _iconButton;

        private SpriteRenderer _spriteButton;
        private Color _startColor;
        private Color _targetColor = Color.red;
        private Color _currentColor;
        private float _transitionDuration = 1.5f;
        private Coroutine _changeColorCoroutine;

        public event Action ButtonFullPressed;

        private void Start()
        {
            _spriteButton = _iconButton.GetComponent<SpriteRenderer>();
            _startColor = _spriteButton.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                StartColorChangeCoroutine(FillColor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                StartColorChangeCoroutine(ReturnToOriginalColor);
            }
        }

        private void StartColorChangeCoroutine(Func<Color, IEnumerator> colorChangeMethod)
        {
            if (_changeColorCoroutine != null)
            {
                StopCoroutine(_changeColorCoroutine);
            }

            Color currentColor = _spriteButton.color;
            _changeColorCoroutine = StartCoroutine(colorChangeMethod(currentColor));
        }

        private bool IsColorApproximatelyEqual(Color colorA, Color colorB)
        {
            return Mathf.Approximately(colorA.r, colorB.r) &&
                   Mathf.Approximately(colorA.g, colorB.g) &&
                   Mathf.Approximately(colorA.b, colorB.b) &&
                   Mathf.Approximately(colorA.a, colorB.a);
        }

        private IEnumerator FillColor(Color currentColor)
        {
            float elapsedTime = 0;

            while (elapsedTime < _transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float fillPercentage = elapsedTime / _transitionDuration;
                _spriteButton.color = Color.Lerp(currentColor, _targetColor, fillPercentage);

                if (IsColorApproximatelyEqual(_spriteButton.color, _targetColor))
                {
                    ButtonFullPressed?.Invoke();
                }

                yield return null;
            }
        }

        private IEnumerator ReturnToOriginalColor(Color currentColor)
        {
            float elapsedTime = 0;

            while (elapsedTime < _transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float fillPercentage = elapsedTime / _transitionDuration;
                _spriteButton.color = Color.Lerp(currentColor, _startColor, fillPercentage);
                yield return null;
            }
        }
    }
}