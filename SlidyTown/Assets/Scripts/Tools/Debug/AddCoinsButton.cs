using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCoinsButton : MonoBehaviour {

	void Awake() {
        this.GetComponent<Button>().onClick.AddListener(() => {
            SaveManager.Data.Coins += 100;
            SaveManager.Save();
        });
    }
}
