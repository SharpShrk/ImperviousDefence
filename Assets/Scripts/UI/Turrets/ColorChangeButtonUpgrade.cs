using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ColorChangeButtonUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject _iconButton;

    private SpriteRenderer _spriteButton;
    private Color _startColor;
    private Color _targetColor = Color.red;
    private Color _currentColor;
    private float _transitionDuration = 1.5f;
    private Coroutine _changeColorCoroutine;

    public event UnityAction ButtonFullPressed;

    private void Start()
    {
        _spriteButton = _iconButton.GetComponent<SpriteRenderer>();
        _startColor = _spriteButton.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {

            if (_changeColorCoroutine != null)
            {
                StopCoroutine(_changeColorCoroutine);
            }

            _currentColor = _spriteButton.color;
            _changeColorCoroutine = StartCoroutine(FillColor(_currentColor));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if(_changeColorCoroutine != null)
            {
                StopCoroutine(_changeColorCoroutine);
            }

            _currentColor = _spriteButton.color;
            _changeColorCoroutine = StartCoroutine(ReturnToOriginalColor(_currentColor));
        }
    }

    private IEnumerator FillColor(Color currentColor)
    {
        float elapsedTime = 0;

        while (elapsedTime < _transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float fillPercentage = elapsedTime / _transitionDuration;
            _spriteButton.color = Color.Lerp(currentColor, _targetColor, fillPercentage);

            if(Mathf.Approximately(_spriteButton.color.r, _targetColor.r) &&
               Mathf.Approximately(_spriteButton.color.g, _targetColor.g) &&
               Mathf.Approximately(_spriteButton.color.b, _targetColor.b) &&
               Mathf.Approximately(_spriteButton.color.a, _targetColor.a))
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
