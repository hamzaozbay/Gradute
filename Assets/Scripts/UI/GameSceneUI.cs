using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour {


    [SerializeField] private GameObject _lifeHolder;
    [SerializeField] private LanguageChange[] _texts;


    private void Start() {
        ChangeLanguage(_texts);
        GameManager.instance.GameSceneUI = this;
    }


    public void LifeChange(int life) {
        for (int i = _lifeHolder.transform.childCount - 1; i >= 0; i--) {
            if (life > 0) {
                _lifeHolder.transform.GetChild(i).gameObject.SetActive(true);
                life--;
            }
            else {
                _lifeHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }



    public void PauseGame() {
        GameManager.instance.Pause();
    }

    public void UnPauseGame() {
        GameManager.instance.UnPause();
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
