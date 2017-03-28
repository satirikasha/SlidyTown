using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SwitcherPickupSpawner : MonoBehaviour, ISnapped {

    void Awake() {
        WorldObjectProvider.OnWorldApplied += Respawn;
    }

    void OnDestroy() {
        WorldObjectProvider.OnWorldApplied -= Respawn;
    }

    void OnEnable() {
        Respawn();
    }

    public void Respawn() {
        Clear();
        Spawn();
    }

    public void Clear() {
        foreach (var pickup in this.GetComponentsInChildren<PickupItem>()) {
            Destroy(pickup.gameObject); // TODO: Pool pickups and followers
        }
    }

    public void Spawn() {
        var selectedIndex = Random.Range(0, this.transform.childCount);
        for (int i = 0; i < this.transform.GetChild(selectedIndex).childCount; i++) {
            var obj = Instantiate(WorldObjectProvider.GetWorldObject<GameObject>("Pickup"), this.transform.GetChild(selectedIndex).GetChild(i), false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        for (int i = 0; i < this.transform.childCount; i++) {
            UnityEditor.Handles.color = new Color((i + 1) % 2, (i + 1) / 2 % 2, (i + 1) / 4 % 2);
            for (int j = 0; j < this.transform.GetChild(i).childCount; j++) {
                UnityEditor.Handles.SphereCap(this.GetInstanceID(), this.transform.GetChild(i).GetChild(j).position + this.transform.GetChild(i).GetChild(j).up * 0.5f, Quaternion.identity, 1);
                UnityEditor.Handles.ArrowCap(this.GetInstanceID(), this.transform.GetChild(i).GetChild(j).position, this.transform.GetChild(i).GetChild(j).rotation, 1);
            }   
        }
    }
#endif
}
