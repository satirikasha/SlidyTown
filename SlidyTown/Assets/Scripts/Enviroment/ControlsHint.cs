using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHint : MonoBehaviour {

    void Awake() {
        if (SaveManager.Data.MaxPoints > 5)
            this.gameObject.SetActive(false);
    }
}
