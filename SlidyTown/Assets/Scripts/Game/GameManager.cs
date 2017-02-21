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

    public void StartLevel() {
        UIManager.SetCurrentPanel("GamePanel");
        IsPlaying = true;
        Time.timeScale = 1;
    }
}
