using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPA : MonoBehaviour {


    public float startGPA = 4.0f;
    public TextMeshProUGUI gpa;
    public float countdownSpeed = 1f;


    private float _time;



    private void Awake() {
        GameManager.instance.Gpa = this;        
    }


    private void Update() {
        _time += Time.deltaTime;

        if (_time > countdownSpeed && !GameManager.instance.IsGameOver) {
            UpdateGpaText(-0.01f);
            
            if(startGPA <= 0f) {
                GameManager.instance.GameOver();
            }

            _time = 0f;
        }
    }

    /// <summary>
    /// Update GPA text. Split floats integer part and decimal part.
    /// </summary>
    public void UpdateGpaText(float changeValue) {
        startGPA += changeValue;
        startGPA = Mathf.Clamp(startGPA, 0.0f, 4.0f);

        int integerPart = Mathf.FloorToInt(startGPA);
        int decimalPart = (int)((startGPA - integerPart) * 100f);
        gpa.text = "<size=150>" + integerPart.ToString() + "<size=80>." + decimalPart.ToString("00");
    }

}
