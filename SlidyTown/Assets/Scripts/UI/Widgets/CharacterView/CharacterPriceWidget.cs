using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPriceWidget : MonoBehaviour {

    public GameObject AffordablePrice;
    public GameObject NonAffordabledPrice;

    public int Price { get; private set; }
	
	void Update () {
        if (Price <= SaveManager.Data.Coins) {
            AffordablePrice.SetActive(true);
            NonAffordabledPrice.SetActive(false);
        }
        else {
            AffordablePrice.SetActive(false);
            NonAffordabledPrice.SetActive(true);
        }
	}

    public void SetPrice(int price) {
        Price = price;
        AffordablePrice.GetComponentInChildren<Text>().text = price.ToString();
        NonAffordabledPrice.GetComponentInChildren<Text>().text = price.ToString();
    }
}
