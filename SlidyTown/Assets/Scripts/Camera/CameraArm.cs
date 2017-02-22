using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraArm : SingletonBehaviour<CameraArm> {
    public float FocusRadius = 5;
    [Space]
    public float Height = 10;
    public float Distance = 5;
    public float VelocityOffset = 0.25f;
    public float DistanceDamping = 2;
    public float AngleDamping = 3;

    void Start() {
        SnapToTarget();
    }

    void Update() {
        if (PlayerController.LocalPlayer != null) {
            this.transform.position = Vector3.Lerp(
                this.transform.position,
                GetTargetPosition(),
                Time.deltaTime * DistanceDamping
                );
        }
    }

    public void SnapToTarget() {
        this.transform.position = GetTargetPosition();
    }

    private Vector3 GetFocusPosition() {
        var offset = Vector3.zero;
        var count = 0;
        foreach (var point in CameraFocusPoint.CameraFocusPoints) {
            var currentOffset = point.transform.position - PlayerController.LocalPlayer.transform.position;
            var offsetMagnitude = currentOffset.magnitude;
            if (point.transform != PlayerController.LocalPlayer.transform && offsetMagnitude < FocusRadius) {
                var distanceMult = 1 - currentOffset.magnitude / FocusRadius;
                offset += currentOffset * distanceMult * distanceMult * point.Weight;
                count++;
            }
        }
        if (count > 0) {
            return offset + PlayerController.LocalPlayer.transform.position;
        }
        else {
            return PlayerController.LocalPlayer.transform.position;
        }
    }

    private Vector3 GetTargetPosition() {
        return Vector3.Scale(GetFocusPosition(), new Vector3(0.75f, 1, 1)) + Vector3.up * (Height + PlayerController.LocalPlayer.MovementController.Speed * VelocityOffset) - Vector3.forward * Distance;
    }
}
