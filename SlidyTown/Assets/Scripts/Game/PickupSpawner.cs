using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

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
}
