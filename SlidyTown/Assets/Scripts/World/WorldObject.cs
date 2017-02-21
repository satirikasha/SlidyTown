using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class WorldObject : MonoBehaviour {

    private string CleanName {
        get {
            return this.name.Split(' ').FirstOrDefault();
        }
    }

    void Awake() {
        WorldObjectProvider.OnWorldChanged += Refresh;
        Refresh();
    }

    void OnDestroy() {
        WorldObjectProvider.OnWorldChanged -= Refresh;
    }

    public void Refresh() {
        if (this.transform.childCount > 0) {
#if UNITY_EDITOR
            if (Application.isPlaying) {
                Destroy(this.transform.GetChild(0).gameObject);
            }
            else {
                UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(this.transform.GetChild(0).gameObject);                
            }
#else
            Destroy(this.transform.GetChild(0).gameObject);
#endif
        }

        var source = WorldObjectProvider.GetWorldObject<GameObject>(this.CleanName);
        if (source != null) {
            var obj = Instantiate(source, this.transform, false);
            obj.name = this.CleanName + "_" + WorldObjectProvider.CurrentWorld;
        }
    }
}