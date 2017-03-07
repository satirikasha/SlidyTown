using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFollower : PlayerFollower {

    protected override void OnDestroyFollower() {
        var splashEffect = VisualEffectsManager.Instance.GetEffect("Splash");
        splashEffect.transform.position = this.transform.position;
        if (splashEffect != null) {
            splashEffect.Play();
        }
        this.gameObject.SetActive(false);
    }
}
