using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWidget : SingletonBehaviour<ScoreWidget> {

    public bool Visible {
        get {
            return _Visible;
        }
    }
    private bool _Visible = true;

    public float TransitionTime = 0.25f;
    public bool ScoreOnly;

    private Text _ScoreText;
    private Text _RecordText;
    private CanvasGroup _CanvasGroup;
    private bool _Initialized;

    protected override void Awake () {
        base.Awake();
        _ScoreText = this.transform.GetChild(0).GetComponent<Text>();
        _RecordText = this.transform.GetChild(1).GetComponent<Text>();
        _CanvasGroup = this.GetComponent<CanvasGroup>();

        if (ScoreOnly)
            _RecordText.gameObject.SetActive(false);

        _Initialized = true;
    }
	
	void Update () {
        if (GameManager.Instance != null) {
            _ScoreText.text = GameManager.Instance.Score.ToString();
        }
	}

    protected override void OnEnable() {
        base.OnEnable();
        if (!ScoreOnly) {
            _RecordText.text = " MAX " + SaveManager.Data.MaxPoints; 
        } 
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
            _RecordText.text = " MAX " + SaveManager.Data.MaxPoints;
        }
    }
}
