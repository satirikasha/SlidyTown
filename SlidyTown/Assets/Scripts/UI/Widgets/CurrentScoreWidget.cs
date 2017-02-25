using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreWidget : MonoBehaviour {

    private Text _Text;

	void Awake () {
        _Text = this.GetComponent<Text>();
	}
	
	void Update () {
        _Text.text = GameManager.Instance.Score.ToString();	
	}
}
