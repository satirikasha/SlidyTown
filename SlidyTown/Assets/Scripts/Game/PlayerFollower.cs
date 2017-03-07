using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public float BaseDistance = 2;
    public float Spacing = 1;

    public int Index { get; set; }

    protected Transform LookAtTransform;
    protected Vector3 TargetDirection;
    protected float Speed;

    protected virtual void Start() {
        LookAtTransform = Index == 0 ? PlayerController.LocalPlayer.transform : PlayerController.LocalPlayer.FollowerController.GetFollower(Index - 1).transform;
        PlayerController.LocalPlayer.OnPlayerDied += () => {
            this.GetComponent<Collider>().enabled = true;
            TargetDirection = (this.transform.forward + this.transform.right * Random.Range(-1, 1)).normalized;
            Speed = PlayerController.LocalPlayer.MovementController.Speed;
        };
    }

    protected virtual void Update() {
        if (!PlayerController.LocalPlayer.Dead) {
            this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(BaseDistance + Spacing * Index);
            this.transform.LookAt(LookAtTransform);
        }
        else {
            this.transform.forward = Vector3.RotateTowards(this.transform.forward, TargetDirection, 0.5f * Speed * Time.deltaTime, 1).normalized;
            this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime * Speed;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Follower") || collider.CompareTag("Pickup") || collider.CompareTag("Player")) {
            return;
        }

        OnDestroyFollower();
    }

    protected virtual void OnDestroyFollower() {

    }
}
