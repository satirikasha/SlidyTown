using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorManager : SingletonBehaviour<UIColorManager> {

    public float Damping;
    public float SleepTimeout;

    public Color MainColor;
    public Color SecondaryColor;

    private float _CurrentTimeout;

    protected override void Awake() {
        base.Awake();
        WakeUp();
        WorldObjectProvider.OnWorldChanged += WakeUp;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        WorldObjectProvider.OnWorldChanged -= WakeUp;
    }

    private void WakeUp() {
        _CurrentTimeout = SleepTimeout;
    }

    void Update() {
        if (_CurrentTimeout > 0) {
            MainColor = Color.Lerp(MainColor, WorldObjectProvider.CurrentWorldData.MainColor, Time.unscaledDeltaTime * Damping);
            SecondaryColor = Color.Lerp(SecondaryColor, WorldObjectProvider.CurrentWorldData.SecondaryColor, Time.unscaledDeltaTime);
            _CurrentTimeout -= Time.unscaledDeltaTime;
        }
    }
}
