using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBySpeed : MonoBehaviour {

    public Material ControlledMaterial { get; private set; }

	void Awake() {
        var renderer = this.GetComponent<Renderer>();
        ControlledMaterial = new Material(renderer.sharedMaterial);
        renderer.sharedMaterial = ControlledMaterial;
    }

    void Update() {
        ControlledMaterial.SetColor("_Color", new Color(1, 1, 1,
            (PlayerController.LocalPlayer.MovementController.Speed - PlayerController.LocalPlayer.MovementController.MinSpeed) /
            (PlayerController.LocalPlayer.MovementController.MaxSpeed - PlayerController.LocalPlayer.MovementController.MinSpeed))
            );
    }
}
