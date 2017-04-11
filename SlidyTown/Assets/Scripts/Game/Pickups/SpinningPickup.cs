using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPickup : PickupItem {

    public float Speed = 45;

    private Transform _YouTubeLogo;

    void Awake() {
        _YouTubeLogo = this.transform.GetChild(0);
        _YouTubeLogo.localRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
    }

    void Update() {
        _YouTubeLogo.Rotate(0, Speed * Time.deltaTime, 0);
    }
}
