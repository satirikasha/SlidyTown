﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : SingletonBehaviour<GameManager> {

    public int Score { get; private set; }

    public bool IsPlaying { get; private set; }

    protected override void Awake() {
        base.Awake();
        IsPlaying = false;
        Time.timeScale = 0;
    }

    void Start() {
        CameraArm.Instance.SnapToTarget();
        PlayerController.LocalPlayer.OnPlayerDied += () => Finish(2);
        PlayerController.LocalPlayer.OnPickedUp += _ => Score++;
    }

    public void StartLevel() {
        UIManager.SetCurrentPanel("GamePanel");
        LogoWidget.Instance.Hide();
        IsPlaying = true;
        Time.timeScale = 1;
    }

    public void Finish(float delay) {
        StartCoroutine(FinishTask(delay));
    }

    private IEnumerator FinishTask(float delay) {
        if(SaveManager.Data.MaxPoints < Score) {
            SaveManager.Data.MaxPoints = Score;
        }
        CurrencyManager.AddCoins(Score);
        Analytics.CustomEvent("GameFinished", new Dictionary<string, object> {
            { "world", WorldManager.CurrentWorld },
            { "score", Score},
            { "coins", CurrencyManager.Coins}
        });
        yield return new WaitForSeconds(delay);
        UIManager.SetCurrentPanel("FinishPanel");
    }
}
