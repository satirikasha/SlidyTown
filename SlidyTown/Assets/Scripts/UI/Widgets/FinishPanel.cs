using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanel : SingletonBehaviour<FinishPanel> {

    public GameObject ActiveBonus;
    public GameObject ExpiredBonus;

    public bool Expired {
        get {
            return _Expired;
        }
        set {
            if(_Expired != value) {
                _Expired = value;
                OnExpiredChanged();
            }
        }
    }
    private bool _Expired = false;


    protected override void OnEnable() {
        base.OnEnable();
        Expired = false;
        OnExpiredChanged();
    }

    public void ApplyBonus() {
        SaveManager.Data.Coins += GameManager.Instance.Score * 2;
        Expired = true;
    }

    private void OnExpiredChanged() {
        ActiveBonus.SetActive(!Expired);
        ExpiredBonus.SetActive(Expired);
    }
}
