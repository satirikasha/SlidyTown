﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public virtual void OnPickedUp() {
        Destroy(this.gameObject);
    }
}
