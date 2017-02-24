#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class WorldDebugger {

    private const string ImmortalKey = "WorldDebuggerImmortal";

    public static bool Immortal {
        get {
            if (EditorPrefs.HasKey(ImmortalKey)) {
                return EditorPrefs.GetBool(ImmortalKey);
            }
            else {
                return false;
            }
        }
        set {
            EditorPrefs.SetBool(ImmortalKey, value);
        }
    }
}
#endif