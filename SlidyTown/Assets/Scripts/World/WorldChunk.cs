﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChunk : MonoBehaviour {

    //[HideInInspector]
    public ChunkData Data;

    void Awake() {
        //StaticBatchingUtility.Combine(this.gameObject);
    }
}
