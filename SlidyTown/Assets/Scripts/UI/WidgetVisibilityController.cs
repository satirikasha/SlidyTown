using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetVisibilityController : MonoBehaviour {

    public bool CoinWidgetVisible;

	void OnEnable() {
        CoinWidget.Instance.SetVisibility(CoinWidgetVisible);
    }
}
