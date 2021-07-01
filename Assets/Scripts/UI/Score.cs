using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public float scoreIncreaseSpeed = 0.25f;

    private TextMeshProUGUI _scoreText;
    private float _time;
    private int _score;
    private float _speedCheckScore;



    private void Awake() {
        GameManager.instance.Score = this;
        _scoreText = GetComponent<TextMeshProUGUI>();
    }


    private void Update() {
        _time += Time.deltaTime;

        if (_time > scoreIncreaseSpeed && !GameManager.instance.IsGameOver) {
            _score++;
            _scoreText.text = _score.ToString();

            _speedCheckScore++;
            if (_speedCheckScore > 250f) {
                GameManager.instance.SpeedUp();
                _speedCheckScore = 0f;
            }

            _time = 0f;
        }
    }


    public void ScoreChange(int value) {
        _score += value;
        _speedCheckScore += Mathf.Abs(value);

        _score = (int)Mathf.Clamp(_score, 0, Mathf.Infinity);
    }



    public float GetScore() {
        return _score;
    }

}
