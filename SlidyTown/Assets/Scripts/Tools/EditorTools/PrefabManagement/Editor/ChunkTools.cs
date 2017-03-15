using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class ChunkTools {

    private static string ChunksPath = "Assets/Prefabs/Chunks";

    public static void BakeChunks() {
        var assetPaths = AssetDatabase.FindAssets("t:GameObject", new string[] { ChunksPath });

        foreach (var assetPath in assetPaths) {
            var chunk = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assetPath));
            foreach (var worldObject in chunk.GetComponentsInChildren<WorldObject>()) {
                worldObject.Clear();
            }
        }

        AssetDatabase.SaveAssets();
    }
}
