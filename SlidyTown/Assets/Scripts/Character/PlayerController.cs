using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController> {

    public static PlayerController LocalPlayer {
        get {
            return Instance;
        }
    }

    public MovementController MovementController { get; private set; }

    protected override void Awake() {
        base.Awake();
        MovementController = this.GetComponent<MovementController>();
    }

    void Update() {
        UpdateInput();
    }

    private void UpdateInput() {
        if (Input.GetKeyDown(KeyCode.Space))
            MovementController.SwitchDirection();
    }
}
