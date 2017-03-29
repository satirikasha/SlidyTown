using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorManager : SingletonBehaviour<UIColorManager> {

    public float Damping;
    public float SleepTimeout;

    public Color MainColor;
    public Color SecondaryColor;

    protected override void Awake() {
        base.Awake();
        WorldObjectProvider.OnWorldChanged += WakeUp;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        WorldObjectProvider.OnWorldChanged -= WakeUp;
    }

    private void WakeUp() {

    }

    void Update() {
        MainColor = Color.Lerp(MainColor, WorldObjectProvider.CurrentWorldData.MainColor, Time.unscaledDeltaTime * Damping);
        SecondaryColor = Color.Lerp(SecondaryColor, WorldObjectProvider.CurrentWorldData.SecondaryColor, Time.unscaledDeltaTime);
    }
}
