using System;
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

    public MovementController MovementController { get; private set; }

    protected override void Awake() {
        base.Awake();
        MovementController = this.GetComponent<MovementController>();
    }

    void Update() {
        UpdateInput();
    }

    void OnTriggerEnter() {
        MovementController.StopMovement();
        Instantiate(WorldObjectProvider.GetWorldObject("DestroyEffect"), this.transform.position, Quaternion.identity);
        this.transform.GetChild(0).gameObject.SetActive(false);
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
