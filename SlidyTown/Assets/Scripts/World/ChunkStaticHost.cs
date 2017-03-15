using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStaticHost : MonoBehaviour {

#if UNITY_EDITOR
    public void Clear() {
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(this.transform.GetChild(i).gameObject, true);
        }
    }
#endif
}
