using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public float Radius = 0.35f;
    public float SafetyDistance = 0.75f;

    private PlayerController _PlayerController;
    private MovementController _MovementController;

    void Start() {
        _PlayerController = PlayerController.LocalPlayer;
        _MovementController = _PlayerController.MovementController;
    }

    void Update() {
        UpdateAI();
    }

    private void UpdateAI() {
        if (CheckDirectionChange())
            _PlayerController.SwitchDirection();
    }

    private bool CheckDirectionChange() {
        var targetDirection = _MovementController.TargetDirection;
        var turningDistance = GetTurningDistance();
        var dangerDistance = turningDistance + SafetyDistance;
        var hit = new RaycastHit();
        if (!_MovementController.IsTurning) {
            if (Physics.SphereCast(this.transform.position, Radius, this.transform.forward, out hit, dangerDistance, LayerMask.GetMask("Obstacle"))) {
                return true;
            }
            if (Physics.Raycast(this.transform.position + this.transform.forward * turningDistance + _MovementController.OppositeDirection * SafetyDistance, _MovementController.OppositeDirection, out hit, dangerDistance, LayerMask.GetMask("Pickup"))) {
                return true;
            }
        }
        return false;
    }

    private float GetTurningDistance() {
        return 1 / _MovementController.Steering;
    }
}
