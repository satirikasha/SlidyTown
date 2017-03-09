﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

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
        if (GameManager.Instance != null) {
            Expired = !Advertisement.IsReady("rewardedVideo") || GameManager.Instance.Score == 0;
        }
        OnExpiredChanged();
    }

    public void ApplyBonus() {
        if (Advertisement.IsReady("rewardedVideo")) {
            var options = new ShowOptions { resultCallback = OnBonusApplied };
            Advertisement.Show("rewardedVideo", options);
        }     
    }

    private void OnBonusApplied(ShowResult result) {
        if (result == ShowResult.Finished) {
            CurrencyManager.AddCoins(GameManager.Instance.Score * 2);
            Expired = true;
        }
    }

    private void OnExpiredChanged() {
        ActiveBonus.SetActive(!Expired);
        ExpiredBonus.SetActive(Expired);
    }
}
