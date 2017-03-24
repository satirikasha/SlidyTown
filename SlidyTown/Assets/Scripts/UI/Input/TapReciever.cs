using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapReciever : MonoBehaviour, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData) {
        PlayerController.LocalPlayer.SwitchDirection();
    }
}
