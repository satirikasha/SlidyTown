using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PickupSpawner : MonoBehaviour, ISnapped {

    [Range(0,1)]
    public float Probability;

    void OnEnable() {
        Clear();
        if (Random.value < Probability)
            Spawn();
    }

    private void Clear() {
        for (int i = 0; i < this.transform.childCount; i++) {
            foreach(Transform pickup in this.transform.GetChild(i)) {
                Destroy(pickup.gameObject);
            }
        }
    }

    private void Spawn() {
        for (int i = 0; i < this.transform.childCount; i++) {
            var obj = Instantiate(WorldObjectProvider.GetWorldObject<GameObject>("Pickup"), this.transform.GetChild(i), false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Gizmos.color = Color.Lerp(Color.black, Color.green, Probability);
            Gizmos.DrawWireSphere(this.transform.GetChild(i).position + Vector3.up * 0.5f, 0.5f);
        }
    }
#endif
}
