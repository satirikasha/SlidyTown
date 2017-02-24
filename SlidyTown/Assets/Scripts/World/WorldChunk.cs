using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChunk : MonoBehaviour {

    //[HideInInspector]
    public ChunkData Data;

    void Awake() {
        StaticBatchingUtility.Combine(this.gameObject);
        //PrepareStaticBatching();
    }

    private void PrepareStaticBatching() {
        StartCoroutine(PrepareStaticBatchingTask());
    }

    private IEnumerator PrepareStaticBatchingTask() {
        yield return null;
        StaticBatchingUtility.Combine(this.gameObject);
    }
}
