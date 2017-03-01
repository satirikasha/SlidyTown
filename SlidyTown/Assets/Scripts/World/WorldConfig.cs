using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class WorldConfig : SingletonScriptableObject<WorldConfig> {

    public List<WorldData> Data {
        get {
            return _Data;
        }
    }
    [SerializeField]
    private List<WorldData> _Data;
}

[Serializable]
public class WorldData {
    public string Name;
    public string Title;
    public int Price;
    public Sprite ColoredPreview;
    public Sprite SilouetePreview;
}
