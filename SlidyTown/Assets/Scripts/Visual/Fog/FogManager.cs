using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : SingletonBehaviour<FogManager> {

    private FogConfig _FogConfig;

	void Start () { 
        Refresh();
        WorldObjectProvider.OnWorldApplied += Refresh;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        WorldObjectProvider.OnWorldApplied -= Refresh;
    }

    public void Refresh () {
        StartCoroutine(RefreshTask());
    }

    private IEnumerator RefreshTask() {
        yield return null; //TODO: figure out why this is needed
        _FogConfig = WorldObjectProvider.GetWorldObject<FogConfig>("FogConfig");
        RenderSettings.ambientLight = _FogConfig.AmbientColor;
        Shader.SetGlobalColor("_FogColor", _FogConfig.FogColor);
        Shader.SetGlobalFloat("_FogHeight", _FogConfig.FogHeight);
        Shader.SetGlobalFloat("_FogPower", _FogConfig.FogPower);
    }
}
