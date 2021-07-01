using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverManager : MonoBehaviour {


    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _cashCounter;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerMaleWhackDonald;
    [SerializeField] private GameObject _playerFemaleWhackDonald;
    [SerializeField] private TextMeshProUGUI _score;

    private bool _restartButtonCooldown = false;



    private void Awake() {
        GameManager.instance.GameOverManager = this;
        _gameOverMenu.SetActive(false);
    }


    public void GameOver(float score) {
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.transform.GetChild(1).gameObject.SetActive(false);
        _player.transform.DOMoveX(0f, 1f);

        _score.text = score.ToString();

        _background.transform.DORotate(new Vector3(-90f, 0f, 0f), 2.5f).SetEase(Ease.InQuint).OnComplete(() => {
            StartCoroutine(ShowGameOverMenu());
        });

        _cashCounter.transform.DOMoveX(0f, 1.75f).OnComplete(() => {
            _player.SetActive(false);
            if (GameManager.instance.SelectedGender == Gender.Male) {
                _playerMaleWhackDonald.SetActive(true);
            }
            else {
                _playerFemaleWhackDonald.SetActive(true);
            }
        });
    }

    private IEnumerator ShowGameOverMenu() {
        yield return new WaitForSeconds(10f);
        _gameOverMenu.SetActive(true);
    }


    public void Restart() {
        if(_restartButtonCooldown) return;

        GameManager.instance.Restart();

        _restartButtonCooldown = true;
        Invoke("ButtonReset", 1f);
    }

    private void ButtonReset() {
        _restartButtonCooldown = false;
    }

}
