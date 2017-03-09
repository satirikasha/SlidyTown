using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinWidget : SingletonBehaviour<CoinWidget> {

    public bool Visible {
        get {
            return _Visible;
        }
    }
    private bool _Visible = true;

    public float TransitionTime = 0.25f;
    public Text CoinText;
    public Text StatusText;

    private Animator _Animator;
    private CanvasGroup _CanvasGroup;
    private bool _Initialized;

	protected override void Awake () {
        base.Awake();
        CoinText.text = CurrencyManager.Coins.ToString();
        _Animator = this.GetComponent<Animator>();
        _CanvasGroup = this.GetComponent<CanvasGroup>();

        CurrencyManager.OnCoinsAdded += _ => StartCoroutine(OnCoinsAdded(_));

        _Initialized = true;
	}

    private IEnumerator OnCoinsAdded(int ammount) {
        if (ammount > 0) {
            yield return new WaitUntil(() => _CanvasGroup.alpha == 1);
            yield return new WaitForSeconds(0.5f);
            StatusText.text = "+" + ammount;
            _Animator.SetTrigger("ShowStatusBar");
            yield return new WaitForSeconds(0.75f);
        }
        CoinText.text = CurrencyManager.Coins.ToString();
    }

    public void SetVisibility(bool value) {
        if (_Initialized && _Visible != value)
            StartCoroutine(SetVisibilityTask(value));
    }

    private IEnumerator SetVisibilityTask(bool value) {
        _Visible = value;

        var delay = TransitionTime;
        var target = value ? 1f : 0f;

        while (delay >= 0) {
            var t = 1 - delay / TransitionTime;
            t = t * t * (3f - 2f * t);

            _CanvasGroup.alpha = Mathf.Lerp(1 - target, target, t);

            delay -= Time.unscaledDeltaTime;
            yield return null;
        }

        _CanvasGroup.alpha = target;
    }
}
