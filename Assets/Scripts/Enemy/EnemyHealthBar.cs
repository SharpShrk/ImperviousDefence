using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Canvas _healthCanvas;

    private void Start()
    {
        _healthCanvas.enabled = false;
    }

    private void Update()
    {
        _healthCanvas.transform.LookAt(_healthCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward,
                                       Camera.main.transform.rotation * Vector3.up);
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        _healthBarImage.fillAmount = fillAmount;
        _healthCanvas.enabled = true;
    }
}
