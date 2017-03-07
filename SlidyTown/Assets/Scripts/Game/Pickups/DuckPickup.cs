using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPickup : PickupItem {

    public override void OnPickedUp() {
        var animator = this.GetComponent<Animator>();
        animator.SetTrigger("Hide");
    }

    public void OnPickUpEnd() {
        var splashEffect = VisualEffectsManager.Instance.GetEffect("Splash");
        splashEffect.transform.position = this.transform.position;
        splashEffect.Play();
        base.OnPickedUp();
    }
}
