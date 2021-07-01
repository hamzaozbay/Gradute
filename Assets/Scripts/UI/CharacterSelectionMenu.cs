using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour {


    [SerializeField] private SceneTransition _transition;
    private bool _startButtonCooldown = false;



    public void SelectMale() {
        GameManager.instance.SelectedGender = Gender.Male;
    }

    public void SelectFemale() {
        GameManager.instance.SelectedGender = Gender.Female;
    }


    public void LoadNextScene() {
        if(_startButtonCooldown) return;

        if (PlayerPrefs.GetInt("TutorialPassed", 0) == 0) {
            GameManager.instance.LoadTutorialScene();
        }
        else {
            GameManager.instance.LoadGameScene();
        }

        _startButtonCooldown = true;
        Invoke("ButtonReset", 2f);
    }

    private void ButtonReset() {
        _startButtonCooldown = false;
    }

}
