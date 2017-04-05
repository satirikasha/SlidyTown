using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostController : MonoBehaviour {

    public float Duration;


    public bool BoostActive {
        get {
            return TimeLeft > 0;
        }
    }

    public float TimeLeft { get; private set; }

    public void ApplyBoost() {
        if (!BoostActive) {
            TimeLeft = Duration;
            StartBoost();
        }
        else {
            TimeLeft = Duration;
        }
    }

    void Update() {
        if (TimeLeft > 0) {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft > 0) {
                UpdateBoost();
            }
            else {
                TimeLeft = 0;
                StopBoost();
            }
        }
    }

    protected abstract void StartBoost();
    protected abstract void UpdateBoost();
    protected abstract void StopBoost();
}
