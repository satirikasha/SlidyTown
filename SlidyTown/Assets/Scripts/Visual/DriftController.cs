using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftController : MonoBehaviour {

    public Transform[] Wheels;

    private Transform _Transform;
    private Transform _Parent;
    private Vector3 _PreviousRight;
    private Quaternion _TargetRotation;
    private float _TurnDir;
    private float _TargetAngVelocity;
    private float _AngVelocity;

    void Start () {
        _Transform = this.transform;
        _Parent = _Transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        _TurnDir = Mathf.Sign(Vector3.Dot(_Parent.forward, _PreviousRight));
        _TargetAngVelocity = 0;
        if (Controller.MovementController.IsTurning) {
            _TargetAngVelocity = 650 * PlayerController.LocalPlayer.MovementController.NormalizedSpeed;
            _TargetAngVelocity *= Controller.MovementController.IsMovingRight ? 1 : -1;
        }
        _TargetAngVelocity += -Vector3.Dot(_Transform.forward, PlayerController.LocalPlayer.transform.right) * 650;
        _AngVelocity = Mathf.Lerp(_AngVelocity, _TargetAngVelocity, Time.deltaTime * 10);
        _Transform.localRotation = _Transform.localRotation * Quaternion.Euler(0, _AngVelocity * Time.deltaTime, 0);

        foreach (var wheel in Wheels) {
            wheel.localRotation = Quaternion.Euler(0, 0, -_Transform.localRotation.eulerAngles.y);
        }
    }

    private IEnumerator DirectionCacheTask() {
        _PreviousRight = _Parent.right;
    }
}
