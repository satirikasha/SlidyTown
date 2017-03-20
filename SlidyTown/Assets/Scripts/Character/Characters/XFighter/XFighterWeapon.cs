using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFighterWeapon : MonoBehaviour {

    public float Cooldown = 0.25f;
    public float Distance = 5;

    private float _CooldownLeft;
    private int _WeaponIndex;

    void Awake() {
        _CooldownLeft = Cooldown;
    }

	void Update () {
        if (!PlayerController.LocalPlayer.Dead) {
            _CooldownLeft -= Time.deltaTime;

            if (_CooldownLeft <= 0) {
                _CooldownLeft = Cooldown;
                if (CheckFire()) {
                    Fire();
                }
            }
        }
	}

    private void Fire() {
        var laser = VisualEffectsManager.Instance.GetEffect<LaserVisualEffect>("LaserEffect");
        var source = this.transform.GetChild(_WeaponIndex);
        laser.From = source.position;

        RaycastHit hit;
        if (Physics.Raycast(source.position, source.forward, out hit, Distance, ~LayerMask.GetMask("Character"), QueryTriggerInteraction.Collide)) {
            laser.To = laser.From + source.forward * hit.distance;
        }
        else {
            laser.To = laser.From + source.forward * Distance;
        }

        laser.Play();

        _WeaponIndex = (_WeaponIndex + 1) % this.transform.childCount;
    }

    private bool CheckFire() {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, 0.5f, this.transform.forward, out hit, Distance, ~LayerMask.GetMask("Character"), QueryTriggerInteraction.Collide)) {         
            return hit.transform.CompareTag("Pickup");
        }
        return false;
    }


}
