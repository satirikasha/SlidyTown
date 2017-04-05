using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class SpeedBoost : BoostController {

    public float OverrideSpeed;

    private MovementController _MovementController;

    private float _InitialSpeed;

    protected override void StartBoost() {
        PlayerController.LocalPlayer.Immortal = true;
        PlayerController.LocalPlayer.RecieveInput = false;
        PlayerController.LocalPlayer.GetComponent<AIController>().enabled = true;
        _MovementController = PlayerController.LocalPlayer.MovementController;
        _MovementController.OverrideSpeed = true;
        _InitialSpeed = _MovementController.Speed;
    }

    protected override void UpdateBoost() {
        if (1 - TimeLeft / Duration < 0.8f) {
            _MovementController.Speed = Mathf.Lerp(_MovementController.Speed, OverrideSpeed, Time.deltaTime * 2);
        }
        else {
            _MovementController.Speed = Mathf.Lerp(_MovementController.Speed, _InitialSpeed, Time.deltaTime * 2);
        }
    }

    protected override void StopBoost() {
        PlayerController.LocalPlayer.Immortal = false;
        PlayerController.LocalPlayer.RecieveInput = true;
        PlayerController.LocalPlayer.GetComponent<AIController>().enabled = false;
        _MovementController.OverrideSpeed = false;
        _MovementController.Speed = _InitialSpeed;
    }
}
