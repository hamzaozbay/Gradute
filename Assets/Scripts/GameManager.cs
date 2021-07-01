using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake() {
        if (instance != this) {
            if (instance != null) {
                Destroy(instance.gameObject);
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        Application.targetFrameRate = 60;
    }
    #endregion


    [SerializeField] private float _speedUpMultiplier;
    [SerializeField] private float _maxGradeSpeed = 1.65f;
    [SerializeField] private Gender _selectedGender;
    [SerializeField] private GoogleMobileAdsDemo _googleAds;

    private float _gradeSpeed = 0.65f;
    private GPA _gpa;
    private Score _score;
    private GameOverManager _gameOverManager;
    private SceneTransition _sceneTransition;
    private GameSceneUI _gameSceneUI;
    private PauseMenu _pauseMenu;
    private Vector2 _screenRight, _screenLeft;
    private Camera _cam;
    private bool _isGameOver = false;
    private int _life = 3;



    private void Start() {
        _cam = Camera.main;
        _screenRight = new Vector2((_cam.orthographicSize * _cam.aspect) + 2f, 0f);
        _screenLeft = new Vector2(-(_cam.orthographicSize * _cam.aspect) - 1f, 0f);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        _cam = Camera.main;
        _googleAds.ShowAd();
    }



    public void GPAChange(float value) {
        _gpa.UpdateGpaText(value);
        _score.ScoreChange((int)(value * 100f));
    }

    /// <summary>
    /// If current speed of grades not bigger than max grade speed. Increase speed of grades.
    /// </summary>
    public void SpeedUp() {
        if (_gradeSpeed >= _maxGradeSpeed) return;

        _gradeSpeed *= _speedUpMultiplier;
        _gradeSpeed = Mathf.Clamp(_gradeSpeed, 0f, _maxGradeSpeed);
    }


    /// <summary>
    /// Decrease life by 1. And update life UI.
    /// </summary>
    //<param name="myBool">Parameter value to pass.</param>
    //<returns>Returns an integer based on the passed value.</returns>
    public void LifeDecrease() {
        _life--;
        _gameSceneUI.LifeChange(_life);
        if (_life <= 0) {
            GameOver();
        }
    }


    public void Pause() {
        Time.timeScale = 0f;
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.Open();
    }
    public void UnPause() {
        Time.timeScale = 1f;
        _pauseMenu.Close();
    }


    public void GameOver() {
        if (!_isGameOver) {
            _isGameOver = true;
            SaveHighscore();
            _gameOverManager.GameOver(_score.GetScore());
        }
    }

    /// <summary>
    /// If current score bigger than current highscore. Save to PlayerPrefs as a float.
    /// </summary>
    public void SaveHighscore() {
        float highScore = PlayerPrefs.GetFloat("highscore", 0f);

        if (_score.GetScore() > highScore) {
            PlayerPrefs.SetFloat("highscore", _score.GetScore());
        }
    }


    public void Restart() {
        _sceneTransition.Close();
        StartCoroutine(waitForSceneTransition(SceneIndexes.MAIN_MENU));
    }

    public void LoadGameScene() {
        _sceneTransition.gameObject.SetActive(true);
        _sceneTransition.Close();
        StartCoroutine(waitForSceneTransition(SceneIndexes.GAME));
    }

    public void LoadTutorialScene() {
        _sceneTransition.gameObject.SetActive(true);
        _sceneTransition.Close();
        StartCoroutine(waitForSceneTransition(SceneIndexes.TUTORIAL));
    }

    private IEnumerator waitForSceneTransition(SceneIndexes sceneIndex) {
        yield return new WaitForSeconds(_sceneTransition.GetAnimLength);
        SceneManager.LoadScene((int)sceneIndex);
    }

    private enum SceneIndexes {
        MAIN_MENU = 0,
        TUTORIAL = 1,
        GAME = 2
    }



    public Camera Camera { get { return _cam; } }
    public Vector2 ScreenRight { get { return _screenRight; } }
    public Vector2 ScreenLeft { get { return _screenLeft; } }
    public float SpeedUpMultiplier { get { return _speedUpMultiplier; } }
    public float GradeSpeed { get { return _gradeSpeed; } }
    public bool IsGameOver { get { return _isGameOver; } }

    public Gender SelectedGender { get { return _selectedGender; } set { _selectedGender = value; } }

    public GPA Gpa { set { _gpa = value; } }
    public Score Score { set { _score = value; } }
    public GameOverManager GameOverManager { set { _gameOverManager = value; } }
    public SceneTransition SceneTransition { set { _sceneTransition = value; } }
    public PauseMenu PauseMenu { set { _pauseMenu = value; } }
    public GameSceneUI GameSceneUI { set { _gameSceneUI = value; } }

}
