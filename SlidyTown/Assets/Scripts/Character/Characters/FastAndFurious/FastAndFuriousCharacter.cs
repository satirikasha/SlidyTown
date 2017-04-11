using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAndFuriousCharacter : Character {

    public Transform[] Wheels;

    private Transform _Transform;
    private Quaternion _TargetRotation;
    private float _TargetAngVelocity;
    private float _AngVelocity;

    protected override void Awake() {
        base.Awake();
        _Transform = this.transform;
    }

    void Update() {
        _TargetAngVelocity = 0;
        if (Controller.MovementController.IsTurning) {
            _TargetAngVelocity = 650 * Controller.MovementController.NormalizedSpeed;
            _TargetAngVelocity *= Controller.MovementController.IsMovingRight ? 1 : -1;
        }
        _TargetAngVelocity += -Vector3.Dot(_Transform.forward, Controller.transform.right) * 650;
        _AngVelocity = Mathf.Lerp(_AngVelocity, _TargetAngVelocity, Time.deltaTime * 10);
        _Transform.localRotation = _Transform.localRotation * Quaternion.Euler(0, _AngVelocity * Time.deltaTime, 0);

        foreach (var wheel in Wheels) {
            wheel.localRotation = Quaternion.Euler(0, 0, -_Transform.localRotation.eulerAngles.y);
        }
    }
}
