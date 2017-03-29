using UnityEngine;
using System.Collections.Generic;
using System;

public class HologramMapGenerator : SingletonBehaviour<HologramMapGenerator> {

    public Shader ReplacementShader;
    public HologramSetup ScanerMode;

    private Camera _Camera;

    protected override void Awake() {
        base.Awake();
        _Camera = this.GetComponent<Camera>();
    }

    void Start() {
        ApplySetup(ScanerMode);
        Shader.SetGlobalFloat("_ScanerStartTime", Time.unscaledTime);
        _Camera.SetReplacementShader(ReplacementShader, "RenderType");
    }

    void OnValidate() {
        ApplySetup(ScanerMode);
    }

    void Update() {
        Shader.SetGlobalFloat("_UnscaledTime", Time.unscaledTime);
    }

    public void ApplySetup(HologramSetup setup) {
        Shader.SetGlobalColor("_DefaultHolo", setup.DefaultColor);
        Shader.SetGlobalFloat("_ScanerIntensity", setup.ScanerIntensity);
        Shader.SetGlobalFloat("_ScanerFrequency", setup.ScanerFrequency);
        Shader.SetGlobalFloat("_ScanerSpeed", setup.ScanerSpeed);
        Shader.SetGlobalFloat("_ScanerVerticalMod", setup.VerticalMult); ;
        Shader.SetGlobalFloat("_ScannerGridResonanse", setup.ScanerGridlineResonance);
        Shader.SetGlobalFloat("_GridlineIntensity", setup.GridlineIntensity);
        Shader.SetGlobalFloat("_GridlineDensity", setup.GridlineDensity); ;
        Shader.SetGlobalFloat("_AmbientIntencity", setup.AmbientIntensity);
        Shader.SetGlobalFloat("_AmpientNear", setup.AmbientNear);
        Shader.SetGlobalFloat("_AmbientDepth", setup.AmbientDepth);
        Shader.SetGlobalTexture("_ScanerPing", setup.ScanerPing);
        Shader.SetGlobalTexture("_Gridline", setup.GridTexture);
        Shader.SetGlobalTexture("_Noise", setup.Noise);
        Shader.SetGlobalFloat("_NoiseDensity", setup.NoiseDensity);
    }

    [Serializable]
    public struct HologramSetup {
        public Color DefaultColor;
        public float ScanerIntensity;
        public float ScanerFrequency;
        public float ScanerSpeed;
        public float ScanerGridlineResonance;
        public float GridlineIntensity;
        public float GridlineDensity;
        public float AmbientIntensity;
        public float AmbientNear;
        public float AmbientDepth;
        public float VerticalMult;
        public Texture2D ScanerPing;
        public Texture2D GridTexture;
        public Texture2D Noise;
        public float NoiseDensity;
        public LayerMask Mask;
        public float CameraHeight;
    }
}