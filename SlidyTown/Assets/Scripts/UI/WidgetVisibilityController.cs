using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class WidgetVisibilityController : MonoBehaviour {

    public bool LogoVisible;
    public bool CoinWidgetVisible;
    public bool ScoreVisible;

	void OnEnable() {
        LogoWidget.Instance.SetVisibility(LogoVisible);
        CoinWidget.Instance.SetVisibility(CoinWidgetVisible);
        ScoreWidget.Instance.SetVisibility(ScoreVisible);
    }
}
