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
}

[Serializable]
public class ChunkData {
    [ReadOnly]
    public int ID;
    [Range(0,1)]
    public float Complexity;
    public WorldChunk Prefab;
}
