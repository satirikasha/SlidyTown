using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class BoostManager : InstancedBehaviour<BoostManager> {

    public Dictionary<string, BoostController> BoostCache { get; private set; }
 
    public void ApplyBoost(string boost) {
        if (!BoostCache.ContainsKey(boost)) {
            BoostCache[boost] = CreateBoost(boost);
        }
        BoostCache[boost].ApplyBoost();
    }

    private BoostController CreateBoost(string boost) {
        return Instantiate(BoostConfig.Instance.Boosts.FirstOrDefault(_ => _.name == boost), this.transform, false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ApplyBoost("Speed");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ApplyBoost("Magnet");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ApplyBoost("Gold");
        }
    }
}
