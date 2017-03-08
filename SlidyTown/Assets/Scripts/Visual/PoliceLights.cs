using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour {

    public MeshRenderer BlueLight;
    public MeshRenderer RedLight;
    public float MaxAlpha = 1;

    private Material _BlueMaterial;
    private Material _RedMaterial;
    private Color _BlueColor;
    private Color _RedColor;
    private float _Offset;

    void Start () {
        _Offset = Random.Range(0, 2 * Mathf.PI);
        _BlueMaterial = new Material(BlueLight.sharedMaterial);
        _RedMaterial = new Material(RedLight.sharedMaterial);
        _BlueColor = _BlueMaterial.GetColor("_TintColor");
        _RedColor = _RedMaterial.GetColor("_TintColor");
        BlueLight.sharedMaterial = _BlueMaterial;
        RedLight.sharedMaterial = _RedMaterial;
    }
	
	void Update () {
        _BlueColor.a = ((Mathf.Sin(Time.timeSinceLevelLoad * 5 + _Offset) + 1) / 2) * MaxAlpha;
        _RedColor.a = ((Mathf.Cos(Time.timeSinceLevelLoad * 5 + _Offset) + 1) / 2) * MaxAlpha;
        _BlueMaterial.SetColor("_TintColor", _BlueColor);
        _RedMaterial.SetColor("_TintColor", _RedColor);
    }
}
