using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {


    [SerializeField] private CanvasGroup canvasGroup;



    private void Awake() {
        GameManager.instance.PauseMenu = this;
        this.gameObject.SetActive(false);
    }

    public void UnPauseGame() {
        GameManager.instance.UnPause();
    }


    public void Open() {
        canvasGroup.alpha = 1f;
    }


    public void Close() {
        canvasGroup.alpha = 0f;
        StartCoroutine(disable());
    }
    IEnumerator disable() {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
    }


}
