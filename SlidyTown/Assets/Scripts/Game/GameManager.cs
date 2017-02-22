using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

    public bool IsPlaying { get; private set; }

    protected override void Awake() {
        base.Awake();
        IsPlaying = false;
        Time.timeScale = 0;
    }

    void Start() {
        PlayerController.LocalPlayer.OnPlayerDied += () => Loose(2);
    }

    public void StartLevel() {
        UIManager.SetCurrentPanel("GamePanel");
        LogoWidget.Instance.Hide();
        IsPlaying = true;
        Time.timeScale = 1;
    }

    public void Loose(float delay) {
        StartCoroutine(LooseTask(delay));
    }

    private IEnumerator LooseTask(float delay) {
        yield return new WaitForSeconds(delay);
        UIManager.SetCurrentPanel("FinishPanel");
    }
}
