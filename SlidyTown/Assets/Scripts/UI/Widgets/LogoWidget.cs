using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoWidget : SingletonBehaviour<LogoWidget> {

    private Animator _Animator;

    public bool FullyVisible {
        get {
            var info = _Animator.GetCurrentAnimatorStateInfo(0);
            return info.IsName("Show") && info.normalizedTime >= 1;
        }
    }

    protected override void Awake() {
        base.Awake();
        _Animator = this.GetComponent<Animator>();       
    }

    IEnumerator Start() {
        yield return null;
        yield return new WaitForSecondsRealtime(0.5f);
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
            SetVisibility(true);
    }

    public void SetVisibility(bool visibility) {
        _Animator.SetBool("Visible", visibility);
    }

    public void Show() {
        SetVisibility(true);
    }

    public void Hide() {
        SetVisibility(false);
    }
}
