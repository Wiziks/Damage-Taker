using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisclaimerScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Localization _localization;
    [SerializeField] private TutorialScript _tutorialScript;

    [Header("Panels")]
    [SerializeField] private GameObject _languagePanel;
    [SerializeField] private GameObject _disclaimerPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _disclaimerText;
    [SerializeField] private string _textUKR;
    [SerializeField] private string _textRUS;
    [SerializeField] private string _textENG;
    [Header("Start GameObjects")]
    public GameObject[] StartGameObjects;
    public static DisclaimerScript Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (GameObject gameObject in StartGameObjects)
        {
            gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey(_tutorialScript.GetKeyName()))
            ShowDisclaimerText();
        else
            _languagePanel.SetActive(true);
    }

    void ShowDisclaimerText()
    {
        _languagePanel.SetActive(false);


        if (PlayerPrefs.HasKey("ShowDisclaimer"))
        {
            if (PlayerPrefs.GetInt("ShowDisclaimer") == 1)
            {
                _disclaimerPanel.SetActive(true);
            }
            else
            {
                foreach (GameObject gameObject in StartGameObjects)
                {
                    gameObject.SetActive(true);
                }
            }
        }
        else
            _disclaimerPanel.SetActive(true);
        PlayerPrefs.SetInt("ShowDisclaimer", 1);
        PlayerPrefs.Save();

        
        _disclaimerText.text = "<b>Disclaimer</b>\n\n";
        if (_localization.GetLanguage() == Language.Ukrainian)
            _disclaimerText.text += _textUKR;
        else if (_localization.GetLanguage() == Language.Russian)
            _disclaimerText.text += _textRUS;
        else if (_localization.GetLanguage() == Language.English)
            _disclaimerText.text += _textENG;
    }

    public void SetUkrainianLanguage()
    {
        _localization.SetLanguage(Language.Ukrainian);
        ShowDisclaimerText();
    }

    public void SetRussianLanguage()
    {
        _localization.SetLanguage(Language.Russian);
        ShowDisclaimerText();
    }

    public void SetEnglishLanguage()
    {
        _localization.SetLanguage(Language.English);
        ShowDisclaimerText();
    }
}
