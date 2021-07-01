using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {


    [SerializeField] private CharacterSelectionMenu _characterSelectionMenu;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private LanguageChange[] _texts;


    private void Start() {
        ChangeLanguage(_texts);
        score.text = PlayerPrefs.GetFloat("highscore", 0f).ToString();
    }



    public void ShowCharacterSelection() {
        this.gameObject.SetActive(false);
        _characterSelectionMenu.gameObject.SetActive(true);
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
