using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[DefaultExecutionOrder(500)]
public class WorldChunk : MonoBehaviour {

    public const string ChunksPath = "Chunks";
    public const string EnviromentPath = ChunksPath + "/Enviroment";

    [HideInInspector]
    public ChunkData Data;

    public List<Transform> StaticRoots;

    void Awake() {
        CombineMeshes();
        WorldObjectProvider.AfterWorldApplied += CombineMeshes;
    }

    void OnDestroy() {
        WorldObjectProvider.AfterWorldApplied -= CombineMeshes;
    }

    private void CombineMeshes() {
        StaticBatchingUtility.Combine(StaticRoots.SelectMany(_ => _.GetComponentsInChildren<MeshFilter>()).Select(_ => _.gameObject).ToArray(), this.gameObject);
    }
}
