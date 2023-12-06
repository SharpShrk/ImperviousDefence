using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Canvas _healthCanvas;

    private float _hideTime = 2f;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _hideCoroutine;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_hideTime);
        _healthCanvas.enabled = false;
    }

    private void Update()
    {
        _healthCanvas.transform.LookAt(
            _healthCanvas.transform.position + (Camera.main.transform.rotation * Vector3.forward), Camera.main.transform.rotation * Vector3.up);
    }

    public void HideHealthBar()
    {
        _healthCanvas.enabled = false;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        _healthBarImage.fillAmount = fillAmount;
        _healthCanvas.enabled = true;

        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        _hideCoroutine = StartCoroutine(HideHealthBarCoroutine());
    }

    private IEnumerator HideHealthBarCoroutine()
    {
        yield return _waitForSeconds;

        HideHealthBar();
    }
}
