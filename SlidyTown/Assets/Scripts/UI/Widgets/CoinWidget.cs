using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinWidget : MonoBehaviour {

    private Text _Text;

	void Awake () {
        _Text = this.GetComponentInChildren<Text>();
	}
	
	void Update () {
        _Text.text = SaveManager.Data.Coins.ToString();	
	}
}
