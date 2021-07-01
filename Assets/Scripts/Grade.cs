using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : MonoBehaviour {


    public bool losePointIfMissed = true;
    [SerializeField] private float _value;
    private ObjectPool _objectPool;
    private float _speed = 1f;



    private void Awake() {
        _objectPool = transform.parent.GetComponent<ObjectPool>();
    }


    private void Update() {
        if (GameManager.instance.IsGameOver) {
            this.gameObject.SetActive(false);
        }

        transform.position += -transform.right * _speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Cap")) {
            GameManager.instance.GPAChange(_value);
            _objectPool.ReturnObject(this.gameObject);
        }
        else if (other.gameObject.CompareTag("ClearObjects")) {
            _objectPool.ReturnObject(this.gameObject);

            if (losePointIfMissed) {
                GameManager.instance.GPAChange(-_value);
            }
        }
    }



    public float GetValue() {
        return _value;
    }
    public void SetSpeed(float value) {
        _speed = 1f * value;
    }
}
