using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class WidgetVisibilityController : MonoBehaviour {

    public bool LogoVisible;
    public bool CoinWidgetVisible;

	void OnEnable() {
        LogoWidget.Instance.SetVisibility(LogoVisible);
        CoinWidget.Instance.SetVisibility(CoinWidgetVisible);
    }
}
