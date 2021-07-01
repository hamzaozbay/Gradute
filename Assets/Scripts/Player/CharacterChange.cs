using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChange : MonoBehaviour {


    [SerializeField] private Gender _gender;

    [SerializeField] private AnimatorOverrideController _maleController;
    [SerializeField] private AnimatorOverrideController _femaleController;

    private Animator _anim;


    private void Start() {
        _anim = GetComponent<Animator>();
        _gender = GameManager.instance.SelectedGender;


        if (_gender == Gender.Male) {
            _anim.runtimeAnimatorController = _maleController;
        }
        else {
            _anim.runtimeAnimatorController = _femaleController;
        }
    }

}

public enum Gender {
    Male,
    Female
}