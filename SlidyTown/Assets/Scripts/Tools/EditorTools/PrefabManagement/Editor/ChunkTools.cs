using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class ChunkTools {

    private const string DraftChunksPath = "Assets/Prefabs/DraftChunks";

    public static void BakeChunks() {
        var worlds = AssetDatabase.GetSubFolders(WorldObjectProvider.AssetsWorldsPath)
            .Select(_ => _.Replace(WorldObjectProvider.AssetsWorldsPath + "/", ""))
            .ToList();
        var assetPaths = AssetDatabase.FindAssets("t:GameObject", new string[] { DraftChunksPath });

        EditorUtility.DisplayProgressBar("Baking Chunks", "", 0);

        for (int i = 0; i < assetPaths.Length; i++) {
            var chunk = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assetPaths[i]));
            if (chunk.GetComponent<WorldChunk>() != null) {
                EditorUtility.DisplayProgressBar("Baking Chunks", chunk.name, (i + 1) / (float)assetPaths.Length);
                var obj = GameObject.Instantiate(chunk);
                var staticHost = obj.GetComponentInChildren<ChunkStaticHost>();
                obj.name = obj.name.Replace("(Clone)", "").Replace("Draft", "World");
                obj.transform.position = Vector3.zero;

                foreach (var world in worlds) {
                    foreach (var worldObject in staticHost.GetComponentsInChildren<WorldObject>()) {
                        worldObject.Clear();
                    }
                    WorldObjectProvider.CurrentWorld = world;
                    foreach (var worldObject in staticHost.GetComponentsInChildren<WorldObject>()) {
                        worldObject.Refresh();
                    }

                    var combineInstances = staticHost
                        .GetComponentsInChildren<MeshFilter>()
                        .OrderBy(_ => _.transform.position.z)
                        .Select(_ => {
                            return new CombineInstance() { mesh = _.sharedMesh, transform = _.transform.localToWorldMatrix };
                        })
                        .ToArray();


                    var vertexCount = 0;
                    var meshIndex = 0;
                    var currentInstances = new List<CombineInstance>();
                    for (int j = 0; j < combineInstances.Length; j++) {
                        if (vertexCount + combineInstances[j].mesh.vertexCount < 65000 && j < combineInstances.Length - 1) {
                            vertexCount += combineInstances[j].mesh.vertexCount;
                            currentInstances.Add(combineInstances[j]);
                        }
                        else {
                            meshIndex++;
                            vertexCount = combineInstances[j].mesh.vertexCount;
                            var mesh = new Mesh();
                            mesh.name = obj.name + "_" + world + "_" + meshIndex;
                            mesh.CombineMeshes(currentInstances.ToArray());
                            currentInstances.Clear();
                            AssetDatabase.CreateAsset(mesh, "Assets/" + mesh.name + ".asset");
                        }
                    }
                }
                GameObject.DestroyImmediate(obj);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }
}
