using Agava.WebUtility;
using UnityEngine;

public class DeviceChecker : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _desktopController;
    [SerializeField] private MonoBehaviour _mobileController;
    [SerializeField] private MonoBehaviour _desktopIKTargetMover;
    [SerializeField] private MonoBehaviour _mobileIKTargetMover;
    [SerializeField] private MonoBehaviour _desktopPlayerShooting;
    [SerializeField] private MonoBehaviour _mobilePlayerShooting;    
    [SerializeField] private GameObject _sticksUI;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Device.IsMobile)
        {
            _desktopController.enabled = false;
            _mobileController.enabled = true;

            _desktopIKTargetMover.enabled = false;
            _mobileIKTargetMover.enabled = true;

            _desktopPlayerShooting.enabled = false;
            _mobilePlayerShooting.enabled = true;

            _sticksUI.SetActive(true);
        }
        else
        {
            _desktopController.enabled = true;
            _mobileController.enabled = false;

            _desktopIKTargetMover.enabled = true;
            _mobileIKTargetMover.enabled = false;

            _desktopPlayerShooting.enabled = true;
            _mobilePlayerShooting.enabled = false;

            _sticksUI.SetActive(false);
        }
#endif
    }
}