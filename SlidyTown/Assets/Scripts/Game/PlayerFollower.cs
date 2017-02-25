using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public float Distance = 2;

    public int Index;
	
	protected virtual void Update () {
       this.transform.position = PlayerController.LocalPlayer.PathData.GetPosition(Distance);
	}
}
