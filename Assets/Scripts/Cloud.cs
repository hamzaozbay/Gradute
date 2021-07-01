using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {


    [SerializeField] private Vector2 posYMinMax;
    [SerializeField] private Sprite[] cloudSprites;
    [SerializeField] private Vector2 speedMinMax;

    private SpriteRenderer _spriteRenderer;
    private float _speed;



    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _speed = Random.Range(speedMinMax.x, speedMinMax.y);
    }


    private void Update() {
        if(GameManager.instance.IsGameOver) {
            this.gameObject.SetActive(false);
        }
        transform.position += -transform.right * _speed * Time.deltaTime;
    }


    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("ClearObjects")) {
            Reset(other.GetComponent<ClearObjects>().ScreenRight);
        }
    }


    private void Reset(Vector2 pos) {
        transform.position = pos + new Vector2(0f, Random.Range(posYMinMax.x, posYMinMax.y));
        _spriteRenderer.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
        _speed = Random.Range(speedMinMax.x, speedMinMax.y);
    }


}
