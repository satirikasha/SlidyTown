using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text _ScoreText;

    void Awake () {
        _ScoreText = this.GetComponent<Text>();
    }
	
	void Update () {
        _ScoreText.text = GameManager.Instance.Score.ToString();
	}
}
