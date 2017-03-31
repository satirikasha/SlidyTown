using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FogConfig", menuName = "FogConfig", order = 1)]
public class FogConfig : ScriptableObject {

    public Color FogColor = Color.yellow;
    [Range(0, 1)]
    public float FogHeight = 0.25f;
    [Range(1, 5)]
    public float FogPower = 1.5f;

    void OnValidate() {
        if(FogManager.Instance != null) {
            FogManager.Instance.Refresh();
        }
    }
}
