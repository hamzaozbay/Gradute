using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObjects : MonoBehaviour {


    [SerializeField] private bool isScreenLeft;

    private Camera _cam;
    private Vector2 _screenLeft;
    private Vector2 _screenRight;



    private void Start() {
        _cam = Camera.main;
        _screenRight = new Vector2((_cam.orthographicSize * _cam.aspect) + 1f, 0f);
        _screenLeft = new Vector2(-(_cam.orthographicSize * _cam.aspect) - 1f, 0f);

        if (isScreenLeft) {
            transform.position = _screenLeft;
        }
        else {
            transform.position = _screenRight;
        }
    }


    public Vector2 ScreenLeft { get { return _screenLeft; } }
    public Vector2 ScreenRight { get { return _screenRight; } }
}
