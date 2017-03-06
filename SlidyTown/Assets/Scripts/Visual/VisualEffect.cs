using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class VisualEffect : MonoBehaviour {

    protected virtual void OnDestroy() {
        VisualEffectsManager.Instance.Unregister(this);
    }

    public virtual void Play() {
        this.gameObject.SetActive(true);
        StartCoroutine(PlayTask());
    }

    protected virtual IEnumerator PlayTask() {
        yield return null;
        this.gameObject.SetActive(false);
    }
}
