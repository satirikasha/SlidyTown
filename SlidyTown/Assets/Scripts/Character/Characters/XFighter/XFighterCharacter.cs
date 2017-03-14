using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFighterCharacter : Character {

    private Transform _Transform;
    private Quaternion _InitialRotation;
    private Quaternion _TargetRotation;

    protected override void Awake() {
        base.Awake();
        _Transform = this.transform.GetChild(0);
        _InitialRotation = _Transform.localRotation;
    }

    void Update() {
        if (Controller.MovementController.IsTurning) {
            _TargetRotation = _InitialRotation * (Controller.MovementController.IsMovingRight ? Quaternion.Euler(55, 0, 0) : Quaternion.Euler(-55, 0, 0));
        }
        else {
            _TargetRotation = _InitialRotation;
        }

        _Transform.localRotation = Quaternion.Lerp(_Transform.localRotation, _TargetRotation ,Time.deltaTime * 0.45f * Controller.MovementController.Speed);
    }
}
