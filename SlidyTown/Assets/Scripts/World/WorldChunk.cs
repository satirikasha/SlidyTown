using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class WorldChunk : MonoBehaviour {

    public const string ChunksPath = "Chunks";
    public const string EnviromentPath = ChunksPath + "/Enviroment";


    [HideInInspector]
    public ChunkData Data;
}
