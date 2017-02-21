using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTap : MonoBehaviour, IPointerClickHandler {

    void Update() {
        if (Input.GetKeyUp(KeyCode.Space))
            GameManager.Instance.StartLevel();
    }

    public void OnPointerClick(PointerEventData eventData) {
        GameManager.Instance.StartLevel();
    }
}
