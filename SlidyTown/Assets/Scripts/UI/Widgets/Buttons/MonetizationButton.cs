using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonetizationButton : MonoBehaviour {

	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() => {
            FinishPanel.Instance.ApplyBonus();
        });
	}
}
