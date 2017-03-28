using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class RandomPickupSpawner : MonoBehaviour, ISnapped {

    [Range(0, 1)]
    public float Probability;

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
        if (Random.value < Probability)
            Spawn();
    }

    public void Clear() {
        foreach (var pickup in this.GetComponentsInChildren<PickupItem>()) {
            Destroy(pickup.gameObject); // TODO: Pool pickups and followers
        }
    }

    public void Spawn() {
        for (int i = 0; i < this.transform.childCount; i++) {
            var obj = Instantiate(WorldObjectProvider.GetWorldObject<GameObject>("Pickup"), this.transform.GetChild(i), false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        for (int i = 0; i < this.transform.childCount; i++) {
            UnityEditor.Handles.color = Color.Lerp(Color.black, Color.green, Probability);
            UnityEditor.Handles.SphereCap(this.GetInstanceID(), this.transform.GetChild(i).position + this.transform.GetChild(i).up * 0.5f, Quaternion.identity, 1);
            UnityEditor.Handles.ArrowCap(this.GetInstanceID(), this.transform.GetChild(i).position, this.transform.GetChild(i).rotation, 1);
        }
    }
#endif
}
