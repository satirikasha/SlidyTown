using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
[ExecuteInEditMode]
[DefaultExecutionOrder(-200)]
public class WorldMaterial : MonoBehaviour {

    public Renderer ControlledRenderer { get; private set; }
    public string MaterialName;

    void Awake() {
        ControlledRenderer = this.GetComponent<Renderer>();
        WorldObjectProvider.OnWorldChanged += Refresh;
        Refresh();
    }

    void OnDestroy() {
        WorldObjectProvider.OnWorldChanged -= Refresh;
    }


    public void Refresh() {
        ControlledRenderer.sharedMaterial = WorldObjectProvider.GetWorldObject<Material>(MaterialName);  
    }
}