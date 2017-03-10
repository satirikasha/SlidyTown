using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RibbonWidget : MonoBehaviour {

    private CanvasGroup _CanvasGroup;
    private Image _Ribbon;
    private Text _Text;

    private bool _Initialized;
    void Init() {
        if (!_Initialized) {
            _CanvasGroup = this.GetComponent<CanvasGroup>();
            _Ribbon = this.transform.GetChild(0).GetComponent<Image>();
            _Text = this.GetComponentInChildren<Text>();
            _Initialized = true;
        }
    }

    public void SetupRibbon(RibbonType type) {
        Init();
        if (type != RibbonType.None) {
            var data = RibbonConfig.GetRibbonData(type);
            _CanvasGroup.alpha = 1;
            _Ribbon.color = data.Color;
            _Text.text = data.Message;    
        }
        else {
            _CanvasGroup.alpha = 0;
        }
    }
}

public enum RibbonType {
    None,
    Hot,
    New,
    Unlock
}
