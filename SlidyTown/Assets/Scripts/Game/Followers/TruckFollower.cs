using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckFollower : PlayerFollower {

    protected override void Update() {
        if (!PlayerController.LocalPlayer.Dead) {
            this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(BaseDistance + Spacing * Index);
            this.transform.LookAt(LookAtTransform.position - LookAtTransform.forward * 0.5f);
        }
        else {
            this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime * Speed;
        }
    }

    protected override void OnDestroyFollower() {
        var splashEffect = VisualEffectsManager.Instance.GetEffect("TrailerDestroy");
        splashEffect.transform.position = this.transform.position;
        if (splashEffect != null) {
            splashEffect.Play();
        }
        this.gameObject.SetActive(false);
    }
}
