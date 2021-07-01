using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private ObjectPool _capObjectPool;
    [SerializeField] private Transform _capPosition;
    [SerializeField] private float _missedTime = 3f;
    [SerializeField] Transform _speedIndicatorArrow;
    [SerializeField] private float _throwSpeedMultiplier = 0f;
    [SerializeField] private float _throwSpeedMultiplierChangeSpeed = .25f;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _targetPos;
    private Animator _animator;
    private WaitForSeconds _waitForMissed;
    private bool _canThrow = true;
    private Coroutine _missedCoroutine;
    private bool _canResetThrowSpeed = true;
    private float _time = 0f;



    private void Start() {
        _targetPos = transform.position;
        _animator = GetComponent<Animator>();
        _waitForMissed = new WaitForSeconds(_missedTime);
    }


    private void Update() {
        if (Input.touchCount > 0 && !IsPointerOverUIObject() && !GameManager.instance.IsGameOver) {
            SetTargetPosition();

            if (_canThrow) {
                _time += Time.deltaTime;

                if (_canResetThrowSpeed) {
                    _time = 0f;
                    _canResetThrowSpeed = false;
                }

                SetSpeedIndicator();

                if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    _canResetThrowSpeed = true;
                    _canThrow = false;
                    _animator.SetTrigger("throw");
                }
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _velocity, _smoothTime);
    }


    private void SetSpeedIndicator() {
        _throwSpeedMultiplier = Mathf.PingPong(_time * _throwSpeedMultiplierChangeSpeed, 1f);
        _speedIndicatorArrow.localPosition = new Vector3(_speedIndicatorArrow.localPosition.x,
        Scale(0f, 1f, -0.675f, 0.675f, _throwSpeedMultiplier));
    }

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    /// <summary>
    /// If touch ended. Set target position to touchPosition.
    /// </summary>
    private void SetTargetPosition() {
        if (Input.GetTouch(0).phase != TouchPhase.Ended) {
            Vector3 touchPos = new Vector3(Input.GetTouch(0).position.x, 0f, 0f);
            Vector3 worldPos = GameManager.instance.Camera.ScreenToWorldPoint(touchPos);
            _targetPos = new Vector3(worldPos.x, transform.position.y, 0f);
        }
    }

    private void LaunchCap() {
        GameObject cap = _capObjectPool.GetObject();
        cap.transform.position = _capPosition.position;
        cap.SetActive(true);
        cap.GetComponent<Cap>().LaunchCap(_throwSpeedMultiplier);

        _missedCoroutine = StartCoroutine(missedCountDown());
    }

    IEnumerator missedCountDown() {
        yield return _waitForMissed;
        if (!_canThrow) {
            _animator.SetTrigger("missed");
            _canThrow = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Cap")) {
            _animator.SetTrigger("catch");
            StopCoroutine(_missedCoroutine);
        }
    }

    public void CanThrowTrue() {
        _canThrow = true;
    }


    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
