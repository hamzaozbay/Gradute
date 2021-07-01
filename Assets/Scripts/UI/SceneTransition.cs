using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour {


    private Animator _anim;



    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    private void Start() {
        GameManager.instance.SceneTransition = this;
        Invoke("Open", .5f);
    }


    public void Open() {
        _anim.Play("Open", 0, 0f);
    }


    public void Close() {
        _anim.Play("Close", 0, 0f);
    }


    public float GetAnimLength { get { return AnimationLength.ClipLength(_anim, "transition-Open"); } }
}
