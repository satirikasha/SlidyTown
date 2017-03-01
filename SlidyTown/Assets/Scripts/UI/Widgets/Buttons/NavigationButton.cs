using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour {

    public CanvasGroup TargetPanel;

    void Awake() {
        this.GetComponent<Button>().onClick.AddListener(() => UIManager.SetCurrentPanel(TargetPanel));
    }
}
