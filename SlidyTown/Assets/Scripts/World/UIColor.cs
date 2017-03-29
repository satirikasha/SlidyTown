using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIColor : MonoBehaviour {

    public bool UseSecondaryColor;
    [Range(0, 1)]
    public float Alpha = 1;

    private Color _Color;
    private Image _Image;

    void Awake() {
        _Image = this.GetComponent<Image>();  
    }

    void Update() {
        _Color = UseSecondaryColor ? UIColorManager.Instance.SecondaryColor : UIColorManager.Instance.MainColor;
        if (Alpha != 1) {
            _Color.a *= Alpha;
        }
        _Image.color = _Color;
    }
}