using Agava.YandexGames;
using UnityEngine;

public class LanguageDetector : MonoBehaviour
{
    [SerializeField] private LanguageSwitcher _languageSwitcher;

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _languageSwitcher.SwitchLanguageTo(YandexGamesSdk.Environment.i18n.lang);
#endif
    }
}