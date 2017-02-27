using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class WorldSnapper {

    private const string EnabledKey = "WorldSnapperEnabled";
    private const string GridKey = "WorldSnapperGrid";
    private const string PlaneKey = "WorldSnapperPlane";

    public static bool Enabled {
        get {
            if (EditorPrefs.HasKey(EnabledKey)) {
                return EditorPrefs.GetBool(EnabledKey);
            }
            else {
                return false;
            }
        }
        set {
            EditorPrefs.SetBool(EnabledKey, value);
        }
    }

    public static float GridSize {
        get {
            if (EditorPrefs.HasKey(GridKey)) {
                return EditorPrefs.GetFloat(GridKey);
            }
            else {
                return 0.1f;
            }
        }
        set {
            EditorPrefs.SetFloat(GridKey, value);
        }
    }

    public static float PlaneHeight {
        get {
            if (EditorPrefs.HasKey(PlaneKey)) {
                return EditorPrefs.GetFloat(PlaneKey);
            }
            else {
                return 1f;
            }
        }
        set {
            EditorPrefs.SetFloat(PlaneKey, value);
        }
    }

    static WorldSnapper() {
        EditorApplication.update += Update;
    }

    static void Update() {
        if (Enabled && Selection.transforms.Length > 0 && !EditorApplication.isPlaying) {
            SnapSelection();
        }
    }

    private static void SnapSelection() {
        foreach (var transform in Selection.transforms) {
            if (transform.GetComponent<ISnapped>() != null)
                transform.localPosition = SnapPosition(transform.localPosition);
        }
    }

    private static Vector3 SnapPosition(Vector3 vector) {
        var result = new Vector3();
        result.x = Mathf.Round(vector.x / GridSize) * GridSize;
        result.z = Mathf.Round(vector.z / GridSize) * GridSize;
        result.y = PlaneHeight;
        return result;
    }
}
