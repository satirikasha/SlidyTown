﻿
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

class WorldEditor : EditorWindow {

    [MenuItem("Window/WorldEditor")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(WorldEditor));
    }

    void OnGUI() {

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("World", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        var options = AssetDatabase.GetSubFolders(WorldObjectProvider.AssetsWorldsPath)
            .Select(_ => _.Replace(WorldObjectProvider.AssetsWorldsPath + "/", ""))
            .ToList();

        var selectedIndex = options.IndexOf(WorldObjectProvider.CurrentWorld);
        WorldObjectProvider.CurrentWorld = options.ElementAt(EditorGUILayout.Popup("Current World", selectedIndex, options.ToArray()));
        EditorGUI.indentLevel = 0;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Snapping", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 1;

        WorldSnapper.Enabled = EditorGUILayout.Toggle("Enable Snapping", WorldSnapper.Enabled);
        WorldSnapper.GridSize = EditorGUILayout.FloatField("Grid Size", WorldSnapper.GridSize);
        WorldSnapper.PlaneHeight = EditorGUILayout.FloatField("Plane Height", WorldSnapper.PlaneHeight);

        EditorGUI.indentLevel = 0;
    }
}