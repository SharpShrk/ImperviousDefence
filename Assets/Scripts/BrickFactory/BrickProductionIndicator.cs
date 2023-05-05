using UnityEngine;
using UnityEngine.UI;

public class BrickProductionIndicator : MonoBehaviour
{
    [SerializeField] private BrickFactory _brickFactory;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        _brickFactory.OnProductionUpdate += UpdateProgressBar;
    }

    private void OnDestroy()
    {
        _brickFactory.OnProductionUpdate -= UpdateProgressBar;
    }

    private void UpdateProgressBar(float fillAmount)
    {
        _progressBar.fillAmount = fillAmount;
    }
}