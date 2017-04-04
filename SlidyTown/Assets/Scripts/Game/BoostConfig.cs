using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class BoostConfig : SingletonScriptableObject<BoostConfig> {

    public List<BoostController> Boosts {
        get {
            return _Boosts;
        }
    }
    [SerializeField]
    private List<BoostController> _Boosts;
}
