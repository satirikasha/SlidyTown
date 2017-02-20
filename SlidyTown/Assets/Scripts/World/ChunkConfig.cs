using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class ChunkConfig : SingletonScriptableObject<ChunkConfig> {

    public List<ChunkData> Chunks {
        get {
            return _Chunks;
        }
    }
    [SerializeField]
    private List<ChunkData> _Chunks;

    public ChunkData GetChunkData(int id) {
        return _Chunks.FirstOrDefault(_ => _.ID == id);
    }

#if UNITY_EDITOR
    void OnValidate() {
        _Chunks.ForEach(c => {
            c.ID = UnityEditor.AssetDatabase.GetAssetPath(c.Prefab).GetHashCode();

            if (c.Prefab != null)
                c.Prefab.Data = c;
        });
    }
#endif
}

[Serializable]
public class ChunkData {
    [ReadOnly]
    public int ID;
    [Range(0, 1)]
    public float Complexity;
    public WorldChunk Prefab;
}
