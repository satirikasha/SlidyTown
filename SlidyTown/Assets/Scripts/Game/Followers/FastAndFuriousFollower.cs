using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAndFuriousFollower : ChasingFollower {

    protected override void Start() {
        base.Start();
        var carIndex = Mathf.Min(this.transform.childCount - 1, Index);
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(i == carIndex);
        }
    }
}
