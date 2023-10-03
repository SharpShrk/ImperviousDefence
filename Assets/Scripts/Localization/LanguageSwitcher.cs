using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSwitcher : MonoBehaviour
{
    private string _language;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetEnglish()
    {
        LeanLocalization.SetCurrentLanguageAll("English");
        _language = "English";
    }

    public void SetTurkish()
    {
        LeanLocalization.SetCurrentLanguageAll("Turkish");
        _language = "Turkish";
    }

    public void SetRussian()
    {
        LeanLocalization.SetCurrentLanguageAll("Russian");
        _language = "Russian";
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(_language)
        {
            case "English":
                SetEnglish();
                break;
            case "Turkish":
                SetTurkish();
                break;
            case "Russian":
                SetRussian();
                break;
        }
    }
}
