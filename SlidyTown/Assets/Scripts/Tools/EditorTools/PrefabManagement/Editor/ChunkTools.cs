using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class ChunkTools {

    private static string ChunksPath = "Assets/Prefabs/Chunks";
    private const string DraftChunksPath = "Assets/Prefabs/DraftChunks";

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

    public static void BakeComposites() {
        var composites = GameObject.FindObjectsOfType<CompositeObject>();
        var worlds = AssetDatabase.GetSubFolders(WorldObjectProvider.AssetsWorldsPath)
            .Select(_ => _.Replace(WorldObjectProvider.AssetsWorldsPath + "/", ""))
            .ToList();

        EditorUtility.DisplayProgressBar("Baking Composites", "", 0);

        for (int i = 0; i < composites.Length; i++) {
            var composite = composites[i];
            var position = composite.transform.position;
            composite.transform.position = Vector3.zero;

            EditorUtility.DisplayProgressBar("Baking Composites", composite.name, (i + 1) / (float)composites.Length);

            foreach (var world in worlds) { 
                foreach (var worldObject in composite.GetComponentsInChildren<WorldObject>()) {
                    worldObject.Clear();
                }
                WorldObjectProvider.RefreshWorld(world);
                WorldObjectProvider.CurrentWorld = world;
                WorldObjectProvider.ApplyWorldChanges();
                foreach (var worldObject in composite.GetComponentsInChildren<WorldObject>()) {
                    worldObject.Refresh();
                }

                var combineInstances = composite
                    .GetComponentsInChildren<MeshFilter>()
                    .Select(_ => {
                        return new CombineInstance() { mesh = _.sharedMesh, transform = _.transform.localToWorldMatrix };
                    })
                    .ToArray();

                var prefab = new GameObject(composite.name, typeof(MeshFilter), typeof(MeshRenderer));
                var meshFilter = prefab.GetComponent<MeshFilter>();
                var meshRenderer = prefab.GetComponent<MeshRenderer>();
                var materialOwner = composite.GetComponentInChildren<MeshRenderer>();
                var material = materialOwner != null ? materialOwner.sharedMaterial : null;
                var mesh = new Mesh();
                mesh.name = composite.name + "_" + world;
                mesh.CombineMeshes(combineInstances);
                var compositePath = "Assets/Resources/World/" + world + "/Composite";
                var meshPath = compositePath + "/" + mesh.name + ".asset";
                var prefabPath = compositePath + "/" + composite.name + ".prefab";
                if (!AssetDatabase.IsValidFolder(compositePath)) {
                    AssetDatabase.CreateFolder("Assets/Resources/World/" + world, "Composite");
                }
                AssetDatabase.DeleteAsset(meshPath);
                AssetDatabase.CreateAsset(mesh, meshPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                var meshRef = AssetDatabase.LoadAssetAtPath<Mesh>(meshPath);
                meshFilter.sharedMesh = meshRef;
                meshRenderer.sharedMaterial = material;
                PrefabUtility.CreatePrefab(prefabPath, prefab, ReplacePrefabOptions.Default);
                GameObject.DestroyImmediate(prefab);
            }

            composite.transform.position = position;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }
}
