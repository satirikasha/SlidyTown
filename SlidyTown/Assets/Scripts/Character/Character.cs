using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    protected PlayerController Controller;

    protected virtual void Awake() {
        Controller = this.GetComponentInParent<PlayerController>();
    }
}
