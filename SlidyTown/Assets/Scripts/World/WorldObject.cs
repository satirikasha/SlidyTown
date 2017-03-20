using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
[ExecuteInEditMode]
[DefaultExecutionOrder(-200)]
public class WorldObject : MonoBehaviour, ISnapped {

    private string CleanName {
        get {
            return this.name.Split(' ').FirstOrDefault();
        }
    }

    private GameObject _CurrentGameObject;

    void Awake() {
        WorldObjectProvider.OnWorldChanged += Refresh;
        Refresh();
    }

    void OnDestroy() {
        WorldObjectProvider.OnWorldChanged -= Refresh;
    }


    public void Refresh() {
        var source = WorldObjectProvider.GetWorldObject<GameObject>(this.CleanName);
        if (source != null) {
            var obj = Instantiate(source, this.transform, false);
            obj.name = this.CleanName + "_" + WorldObjectProvider.CurrentWorld;
            obj.hideFlags = HideFlags.DontSave;
            _CurrentGameObject = obj;
        }
        else {
            _CurrentGameObject = null;
        }

        if (this.transform.childCount > 0) {
#if UNITY_EDITOR
            if (Application.isPlaying) {
                for (int i = this.transform.childCount - 1; i >= 0; i--) {
                    if (_CurrentGameObject != this.transform.GetChild(i).gameObject)
                        Destroy(this.transform.GetChild(i).gameObject);
                }
            }
            else {
                UnityEditor.EditorApplication.delayCall += () => {
                    if (this != null) {
                        for (int i = this.transform.childCount - 1; i >= 0; i--) {
                            if (_CurrentGameObject != this.transform.GetChild(i).gameObject)
                                DestroyImmediate(this.transform.GetChild(i).gameObject);
                        }
                    }
                };
            }
#else
            for (int i = this.transform.childCount - 1; i >= 0; i--) {
                if (_CurrentGameObject != this.transform.GetChild(i).gameObject)
                    Destroy(this.transform.GetChild(i).gameObject);
            }
#endif
        }
    }

#if UNITY_EDITOR
    public void Clear() {
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(this.transform.GetChild(i).gameObject, true);
        }
    }
#endif
}