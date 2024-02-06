using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeAngle = 1f;

    private Transform _cameraTransform;
    private Vector3 _originalLocalEulerAngles;

    private void Awake()
    {
        _cameraTransform = GetComponent<Transform>();
        _originalLocalEulerAngles = _cameraTransform.localEulerAngles;
    }

    public void InitiateShake()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine(ApplyShakeEffect());
        }
    }

    private IEnumerator ApplyShakeEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float minRandomValue = -1f;
            float maxRandomValue = 1f;

            float x = Random.Range(minRandomValue, maxRandomValue) * shakeAngle;
            float y = Random.Range(minRandomValue, maxRandomValue) * shakeAngle;

            _cameraTransform.localEulerAngles = _originalLocalEulerAngles + new Vector3(x, y, 0);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _cameraTransform.localEulerAngles = _originalLocalEulerAngles;
    }
}
