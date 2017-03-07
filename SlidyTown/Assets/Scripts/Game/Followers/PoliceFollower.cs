using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceFollower : PlayerFollower {

    private float TargetDistance {
        get {
            return BaseDistance + Spacing * Index;
        }
    }
    private float _Distance;

    protected override void Start() {
        base.Start();
        _Distance = TargetDistance + 10;
    }

    protected override void Update() {
        _Distance = Mathf.Lerp(_Distance, TargetDistance, Time.deltaTime * 0.75f);
        if (!PlayerController.LocalPlayer.Dead) {
            this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(_Distance);
            var dir = PlayerController.LocalPlayer.PathData.GetPosition(_Distance - 0.5f) - this.transform.position;
            if (dir != Vector3.zero) {
                this.transform.forward = dir;
            }
        }
        else {
            this.transform.forward = Vector3.RotateTowards(this.transform.forward, TargetDirection, 0.5f * Speed * Time.deltaTime, 1).normalized;
            this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime * Speed;
        }
    }

    protected override void OnDestroyFollower() {
        //var splashEffect = VisualEffectsManager.Instance.GetEffect("Splash");
        //splashEffect.transform.position = this.transform.position;
        //if (splashEffect != null) {
        //    splashEffect.Play();
        //}
        //this.gameObject.SetActive(false);
    }
}
