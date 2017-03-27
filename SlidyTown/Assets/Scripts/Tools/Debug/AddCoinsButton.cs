using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCoinsButton : MonoBehaviour {

	void Start() {
        this.GetComponent<Button>().onClick.AddListener(() => {
            CurrencyManager.AddCoins(100);
        });
    }
}
