using UnityEngine;

public class FirstStartTrainer : MonoBehaviour
{
    private const string FIRST_START_KEY = "FirstStart";

    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _startPanel;

    private bool _isFirstStart;

    public bool IsFirstStart => _isFirstStart;

    void Start()
    {
        _isFirstStart = true;

        _isFirstStart = PlayerPrefs.GetInt(FIRST_START_KEY, 0) == 0;

        if(_isFirstStart)
        {
            Debug.Log("Первый запуск");
            Debug.Log(PlayerPrefs.GetInt(FIRST_START_KEY));
        }
        else
        {
            Debug.Log("Не первый запуск");
            Debug.Log(PlayerPrefs.GetInt(FIRST_START_KEY));
        }
    }

    public void FirstStartTutorial()
    {
        PlayerPrefs.SetInt(FIRST_START_KEY, 1);
        PlayerPrefs.Save();

        _isFirstStart = false;

        Debug.Log(PlayerPrefs.GetInt(FIRST_START_KEY));

        _tutorialPanel.SetActive(true);
        _startPanel.SetActive(false);
    }
}
