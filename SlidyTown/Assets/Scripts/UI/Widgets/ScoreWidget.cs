using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWidget : MonoBehaviour {

    public bool ScoreOnly;

    private Text _ScoreText;
    private Text _RecordText; 

	void Awake () {
        _ScoreText = this.transform.GetChild(0).GetComponent<Text>();
        _RecordText = this.transform.GetChild(1).GetComponent<Text>();

        if (ScoreOnly)
            _RecordText.gameObject.SetActive(false);
    }
	
	void Update () {
        _ScoreText.text = GameManager.Instance.Score.ToString();
	}

    void OnEnable() {
        if (!ScoreOnly) {
            _RecordText.text = " MAX " + SaveManager.Data.MaxPoints; 
        } 
    }
}
