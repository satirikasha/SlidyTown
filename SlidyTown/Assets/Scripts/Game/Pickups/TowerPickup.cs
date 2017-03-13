using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPickup : PickupItem {

    public override void OnPickedUp() {
        base.OnPickedUp();
        var splashEffect = VisualEffectsManager.Instance.GetEffect("PickupDestroyed");
        splashEffect.transform.position = this.transform.position;
        splashEffect.Play();
    }
}
