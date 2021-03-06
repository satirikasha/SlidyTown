﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController> {

    public static PlayerController LocalPlayer {
        get {
            return Instance;
        }
    }

    public event Action OnPlayerDied;
    public event Action<PickupItem> OnPickedUp;

    public bool Dead { get; private set; }
    public MovementController MovementController { get; private set; }
    public FollowerController FollowerController { get; private set; }
    public PathData PathData { get; private set; }

    protected override void Awake() {
        base.Awake();
        MovementController = this.GetComponent<MovementController>();
        FollowerController = this.GetComponent<FollowerController>();
        PathData = this.GetComponent<PathData>();
    }

    void Update() {
        UpdateInput();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Follower"))
            return;

        if (collider.CompareTag("Pickup")) {
            var pickup = collider.GetComponent<PickupItem>();
            pickup.OnPickedUp();
            if (OnPickedUp != null)
                OnPickedUp(pickup);

            return;
        }

#if UNITY_EDITOR
        if (WorldDebugger.Immortal)
            return;
#endif
        Dead = true;
        MovementController.StopMovement();
        var destroyEffect = VisualEffectsManager.Instance.GetEffect("DestroyEffect");
        destroyEffect.transform.position = this.transform.position;
        if (destroyEffect != null) {
            destroyEffect.Play();
        }
        this.GetComponentInChildren<MeshRenderer>().enabled = false;
        //this.transform.GetChild(0).gameObject.SetActive(false);
        if (OnPlayerDied != null)
            OnPlayerDied();
    }

    private void UpdateInput() {
        if (GameManager.Instance.IsPlaying && Input.GetKeyUp(KeyCode.Space))
            SwitchDirection();
    }

    public void SwitchDirection() {
        MovementController.SwitchDirection();
    }


}
