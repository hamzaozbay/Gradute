using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : MonoBehaviour {


    public float force;
    public float waitTimeForDisable = 4f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ObjectPool _objectPool;
    private WaitForSeconds _waitFor;



    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _objectPool = transform.parent.GetComponent<ObjectPool>();
        _waitFor = new WaitForSeconds(waitTimeForDisable);
    }


    private void Update() {
        if(GameManager.instance.IsGameOver) {
            this.gameObject.SetActive(false);
        }
    }


    public void LaunchCap(float forceMultiplier) {
        forceMultiplier = Scale(0f, 1f, 0.5f, 1f, forceMultiplier);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector2.up * (force * forceMultiplier), ForceMode2D.Impulse);
        _animator.Play("capRotation", 0, 0f);
    }

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            StartCoroutine(waitForDisable());
            GameManager.instance.LifeDecrease();
        }
        else if (other.gameObject.CompareTag("Player")) {
            _objectPool.ReturnObject(this.gameObject);
        }
    }

    IEnumerator waitForDisable() {
        yield return _waitFor;
        _objectPool.ReturnObject(this.gameObject);
    }

}
