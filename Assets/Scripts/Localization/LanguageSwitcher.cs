using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSwitcher : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string EnglishNameLocalization = "English";
    private const string RussianNameLocalization = "Russian";
    private const string TurkishNameLocalization = "Turkish";

    private string _currentLanguage;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SwitchLanguageTo(string code)
    {
        switch (code)
        {
            case EnglishCode:
                ChangeLanguage(EnglishNameLocalization);
                break;

            case RussianCode:
                ChangeLanguage(RussianNameLocalization);
                break;

            case TurkishCode:
                ChangeLanguage(TurkishNameLocalization);
                break;
        }
    }

    private void ChangeLanguage(string localizationName)
    {
        LeanLocalization.SetCurrentLanguageAll(localizationName);
        _currentLanguage = localizationName;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeLanguage(_currentLanguage);
    }
}
