using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour {

    private RibbonWidget _Ribbon;

    private bool _Initialized;
    private void Init() {
        if (!_Initialized) {
            _Ribbon = this.GetComponentInChildren<RibbonWidget>();
            _Initialized = true;
        }
    }

	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() => {
            LogoWidget.Instance.Hide();
            UIManager.SetCurrentPanel("CharacterPanel");
        });
	}

    void OnEnable() {
        Init();
        if(RibbonManager.Highlighted.Count > 0) {
            _Ribbon.SetupRibbon(RibbonManager.Highlighted.First().Value);
        }
        else {
            _Ribbon.SetupRibbon(RibbonType.None);
        }
    }
}
