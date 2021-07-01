using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {


    [SerializeField] private GameObject[] _tutorialMenus;
    [SerializeField] private LanguageChange[] _texts;

    private int index = 0;



    private void Start() {
        ChangeLanguage(_texts);
        _tutorialMenus[index].SetActive(true);
    }


    public void NextTutorial() {
        _tutorialMenus[index].SetActive(false);
        index++;
        _tutorialMenus[index].SetActive(true);
    }

    public void LoadGameScene() {
        if (PlayerPrefs.GetInt("TutorialPassed") == 0) {
            PlayerPrefs.SetInt("TutorialPassed", 1);
            GameManager.instance.LoadGameScene();
        }
    }

    public void ChangeLanguage(LanguageChange[] texts) {
        foreach (LanguageChange language in texts) {
            if (Application.systemLanguage == SystemLanguage.Turkish) {
                language.text.text = language.turkishText;
            }
            else {
                language.text.text = language.englishText;
            }
        }
    }

}
