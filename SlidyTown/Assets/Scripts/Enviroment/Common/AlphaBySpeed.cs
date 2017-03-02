using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBySpeed : MonoBehaviour {

    public bool UseTurnFactor;
    public float TurnFactorDamping = 1;

    private float _TurnFactor;

    public Material ControlledMaterial { get; private set; }

	void Awake() {
        var renderer = this.GetComponent<Renderer>();
        ControlledMaterial = new Material(renderer.sharedMaterial);
        renderer.sharedMaterial = ControlledMaterial;
    }

    void Update() {
        if (PlayerController.LocalPlayer == null)
            return;

        UpdateTurnFactor();

        ControlledMaterial.SetColor("_Color", new Color(1, 1, 1,
            (PlayerController.LocalPlayer.MovementController.Speed - PlayerController.LocalPlayer.MovementController.MinSpeed) /
            (PlayerController.LocalPlayer.MovementController.MaxSpeed - PlayerController.LocalPlayer.MovementController.MinSpeed)) *
            (UseTurnFactor ? _TurnFactor : 1)
            );
    }

    private void UpdateTurnFactor() {
        _TurnFactor = Mathf.Lerp(_TurnFactor, PlayerController.LocalPlayer.MovementController.IsTurning ? 1 : 0, Time.deltaTime * TurnFactorDamping);
    }
}
