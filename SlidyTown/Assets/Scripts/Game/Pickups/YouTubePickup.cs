using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouTubePickup : PickupItem {

    private Transform _YouTubeLogo;

    void Awake() {
        _YouTubeLogo = this.transform.GetChild(0);
    }

    void Update() {
        _YouTubeLogo.LookAt(PlayerController.LocalPlayer.transform);
    }

    public override void OnPickedUp() {
        var splashEffect = VisualEffectsManager.Instance.GetEffect("YouTubeDestroy");
        splashEffect.transform.position = this.transform.position;
        splashEffect.Play();
        base.OnPickedUp();
    }

}
