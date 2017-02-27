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

    private int _LastChunkID;
    private float _LastChunkPosition;
    private Queue<WorldChunk> _CurrentChunks = new Queue<WorldChunk>();
    private List<WorldChunk> _Chunks = new List<WorldChunk>();

    void Awake() {
        //PreloadChunks();
    }

    void Start() {
        _LastChunkPosition = -Step;
        Update();
    }

    void Update() {
        if (PlayerController.LocalPlayer.transform.position.z > AllowedPosition)
            PrepareNextChunk();
    }

    private void PreloadChunks() {
        foreach (var chunkData in ChunkConfig.Instance.Chunks) {
            CreateChunk(chunkData.ID);
            CreateChunk(chunkData.ID);
        } 
    }

    private void PrepareNextChunk() {
        var chunkID = EvaluateNextChunkID();
        var chunk = GetChunk(chunkID);
        chunk.transform.position = new Vector3(0, 0, _LastChunkPosition + Step);
        chunk.gameObject.SetActive(true);
        _CurrentChunks.Enqueue(chunk);
        if(_CurrentChunks.Count > 3) {
            _CurrentChunks.Dequeue().gameObject.SetActive(false);
        }

        _LastChunkPosition += Step;
        _LastChunkID = chunkID;
    }

    private int EvaluateNextChunkID() {
        var selection = ChunkConfig.Instance.Chunks.Where(_ => _.ID != _LastChunkID);
        return selection.ElementAt(Random.Range(0, selection.Count())).ID;
    }

    private WorldChunk GetChunk(int id) {
        var chunk = _Chunks.FirstOrDefault(_ => _.Data.ID == id && !_.gameObject.activeSelf);
        if (chunk == null) {
            chunk = CreateChunk(id);
        }
        return chunk;
    }

    private WorldChunk CreateChunk(int id) {
        var chunk = Instantiate<WorldChunk>(ChunkConfig.Instance.GetChunkData(id).Prefab, this.transform);
        chunk.gameObject.SetActive(false);
        _Chunks.Add(chunk);
        return chunk;
    }
}
