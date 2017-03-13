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

    public bool IsTurning {
        get {
            return Direction != TargetDirection;
        }
    }

    public bool IsMovingRight {
        get {
            return _MovingRight;
        }
    }

    private bool _MovingRight = true;
    private bool _AllowMovement = true;

    void Awake() {
        Speed = MinSpeed;
        Direction = Vector3.forward;
    }

    void Update() {
        if (_AllowMovement) {
            UpdateSpeed();
            UpdateDirection();
            UpdateRotation();
            UpdatePosition();
        }
    }

    public void StopMovement() {
        _AllowMovement = false;
    }

    public void SwitchDirection() {
        _MovingRight = !_MovingRight;
    }

    private void UpdateSpeed() {
        Speed = Mathf.Clamp(Speed + Acceleration * Time.deltaTime, MinSpeed, MaxSpeed);
    }

    private void UpdateDirection() {
        Direction = Vector3.RotateTowards(Direction, TargetDirection, Steering * Speed * Time.deltaTime, 1).normalized;
    }

    private void UpdateRotation() {
        this.transform.rotation = Quaternion.LookRotation(Direction);
    }

    private void UpdatePosition() {
        this.transform.position = this.transform.position + Direction * Time.deltaTime * Speed;
    }
}
