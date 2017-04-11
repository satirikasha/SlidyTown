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

    void OnEnable() {
        StartCoroutine(DirectionCacheTask());
    }
	
	// Update is called once per frame
	void Update () {
        _TurnDir = Vector3.Dot(_Parent.forward, _PreviousRight) * 8;
        _TargetAngVelocity = 650 * PlayerController.LocalPlayer.MovementController.NormalizedSpeed;
        _TargetAngVelocity *= _TurnDir;
        _TargetAngVelocity += -Vector3.Dot(_Transform.forward, _Parent.right) * 650;
        _AngVelocity = Mathf.Lerp(_AngVelocity, _TargetAngVelocity, Time.deltaTime * 10);
        _Transform.localRotation = _Transform.localRotation * Quaternion.Euler(0, _AngVelocity * Time.deltaTime, 0);

        foreach (var wheel in Wheels) {
            wheel.localRotation = Quaternion.Euler(0, 0, -_Transform.localRotation.eulerAngles.y);
        }
    }

    private IEnumerator DirectionCacheTask() {
        while (true) {
            yield return null;
            yield return null;
            yield return null;
            _PreviousRight = _Parent.right;
        }
    }
}
