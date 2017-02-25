using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public float BaseDistance = 2;
    public float Spacing = 1;

    public int Index { get; set; }

    private Transform _LookAtTransform;
	
    protected virtual void Start() {
        _LookAtTransform = Index == 0 ? PlayerController.LocalPlayer.transform : PlayerController.LocalPlayer.FollowerController.GetFollower(Index - 1).transform;
    }

	protected virtual void Update () {
        this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(BaseDistance + Spacing * Index);
        this.transform.LookAt(_LookAtTransform);
	}
}
