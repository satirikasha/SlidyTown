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
    public ParticleSystem CoinParticles;

    private Animator _Animator;
    private RectTransform _RectTransform;
    private CanvasGroup _CanvasGroup;
    private bool _Initialized;

    protected override void Awake() {
        base.Awake();

        CoinText.text = CurrencyManager.Coins.ToString();
        _Animator = this.GetComponent<Animator>();
        _CanvasGroup = this.GetComponent<CanvasGroup>();
        _RectTransform = this.GetComponent<RectTransform>();

        CurrencyManager.OnCoinsAdded += _ => StartCoroutine(OnCoinsAdded(_));
        CurrencyManager.OnCoinsSpent += _ => StartCoroutine(OnCoinsSpent(_));

        _Initialized = true;
    }

    private IEnumerator OnCoinsAdded(int ammount) {
        if (ammount > 0) {
            yield return new WaitUntil(() => _CanvasGroup.alpha == 1);
            yield return new WaitForSeconds(0.5f);
            StatusText.text = "+" + ammount;
            _Animator.SetTrigger("ShowStatusBar");
            yield return new WaitForSeconds(0.75f);
            CoinParticles.Emit(ammount);
            CoinText.text = CurrencyManager.Coins.ToString();
        }
    }

    private IEnumerator OnCoinsSpent(int amount) {
        var duration = 1f;
        var timeLeft = duration;
        while (timeLeft > 0) {
            CoinText.text = ((int)Mathf.Lerp(CurrencyManager.Coins, CurrencyManager.Coins + amount, timeLeft / duration)).ToString();
            timeLeft -= Time.unscaledDeltaTime;
            yield return null;
        }
        CoinText.text = CurrencyManager.Coins.ToString();
    }

    public void SetVisibility(bool value) {
        if (_Initialized && _Visible != value) {         
            StartCoroutine(SetVisibilityTask(value));
        }
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
        if (!value) {
            StopAllCoroutines();
            CoinParticles.Clear();
        }
    }
}
