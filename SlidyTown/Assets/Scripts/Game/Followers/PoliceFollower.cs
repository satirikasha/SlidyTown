using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceFollower : PlayerFollower {

    private float TargetDistance {
        get {
            return BaseDistance + Spacing * Index;
        }
    }

    private bool _Dead;
    private float _Distance;
    private Vector3 _PreviousPosition;
    private float _Argessiveness;

    protected override void Start() {
        base.Start();
        _Distance = TargetDistance + 10;
        _Argessiveness = Random.Range(2f, 8f);
    }

    protected override void Update() {
        if (!_Dead) {
            _Distance = Mathf.Lerp(_Distance, TargetDistance, Time.deltaTime * 0.75f);
            if (!PlayerController.LocalPlayer.Dead) {
                this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(_Distance) + Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad * _Argessiveness) * 0.5f;
                var dir = (this.transform.position - _PreviousPosition).normalized;
                if (dir != Vector3.zero) {
                    this.transform.forward = dir;
                }
                _PreviousPosition = this.transform.position;
            }
            else {
                this.transform.forward = Vector3.RotateTowards(this.transform.forward, TargetDirection, 0.5f * Speed * Time.deltaTime, 1).normalized;
                this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime * Speed;
            }
        }
    }

    protected override void OnDestroyFollower() {
        _Dead = true;
        var destroyEffect = VisualEffectsManager.Instance.GetEffect("PoliceDestroy");
        destroyEffect.transform.position = this.transform.position;
        if (destroyEffect != null) {
            destroyEffect.Play();
        }
        this.gameObject.SetActive(false);
    }
}
