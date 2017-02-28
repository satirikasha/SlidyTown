using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour {

	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() => {
            LogoWidget.Instance.Hide();
            UIManager.SetCurrentPanel("CharacterPanel");
        });
	}
}
