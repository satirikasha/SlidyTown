using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public float Step = 100;

    private float AllowedPosition {
        get {
            return _LastChunkPosition;
        }
    }

    private float _LastChunkPosition;
    private List<WorldChunk> _Chunks = new List<WorldChunk>();

    void Start() {
        _LastChunkPosition = -Step;
        Update();
    }

    void Update() {
        if (PlayerController.LocalPlayer.transform.position.z > AllowedPosition)
            PrepareNextChunk();
    }

    private void PrepareNextChunk() {
        var chunk = GetChunk(EvaluateNextChunkID());
        chunk.transform.position = new Vector3(0, 0, _LastChunkPosition + Step);
        chunk.gameObject.SetActive(true);
        _LastChunkPosition += Step;
    }

    private int EvaluateNextChunkID() {
        return ChunkConfig.Instance.Chunks.First().ID;
    }

    private WorldChunk GetChunk(int id) {
        var chunk = _Chunks.FirstOrDefault(_ => _.Data.ID == id && _.gameObject.activeSelf);
        if (chunk == null) {
            chunk = CreateChunk(id);
        }
        return chunk;
    }

    private WorldChunk CreateChunk(int id) {
        var chunk = Instantiate<WorldChunk>(ChunkConfig.Instance.GetChunkData(id).Prefab, this.transform);
        chunk.gameObject.SetActive(false);
        return chunk;
    }
}
