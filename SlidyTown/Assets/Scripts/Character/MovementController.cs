using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float MinSpeed = 1;
    public float MaxSpeed = 10;
    public float Acceleration = 1;
    public float Steering = 0.1f;

    public float Speed { get; private set; }

    public Vector3 Direction { get; private set; }

    public Vector3 TargetDirection {
        get {
            return (Vector3.forward + (_MovingRight ? Vector3.right : Vector3.left)).normalized;
        }
    }

    private bool _MovingRight = true;

    private Rigidbody _Rigidbody;

    void Awake() {
        Speed = MinSpeed;
        Direction = Vector3.forward;

        _Rigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        UpdateSpeed();
        UpdateDirection();
        UpdateRotation();
        UpdatePosition();
    }

    public void SwitchDirection() {
        _MovingRight = !_MovingRight;
    }

    private void UpdateSpeed() {
        Speed = Mathf.Clamp(Speed + Acceleration * Time.fixedDeltaTime, MinSpeed, MaxSpeed);
    }

    private void UpdateDirection() {
        Direction = Vector3.RotateTowards(Direction, TargetDirection, Steering * Speed * Time.fixedDeltaTime, 1).normalized;
    }

    private void UpdateRotation() {
        _Rigidbody.rotation = Quaternion.LookRotation(Direction);
    }

    private void UpdatePosition() {
        _Rigidbody.position = _Rigidbody.position + Direction * Time.fixedDeltaTime * Speed;
    }
}
