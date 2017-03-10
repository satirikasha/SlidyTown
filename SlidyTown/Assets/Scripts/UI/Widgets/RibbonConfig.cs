using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class RibbonConfig : SingletonScriptableObject<RibbonConfig> {
    [SerializeField]
    private List<RibbonData> Data = new List<RibbonData>();

    public static RibbonData GetRibbonData(RibbonType type) {
        return Instance.Data.FirstOrDefault(_ => _.Type == type);
    }
}

[Serializable]
public class RibbonData {
    public RibbonType Type;
    public string Message;
    public Color Color;
}
