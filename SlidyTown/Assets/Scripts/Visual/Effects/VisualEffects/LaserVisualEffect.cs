using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVisualEffect : VisualEffect {

    public float Speed;

    public Vector3 From { get; set; }
    public Vector3 To { get; set; }

    private ParticleSystem _ParticleSystem;
    private MeshRenderer _MeshRenderer;

    void Awake() {
        _ParticleSystem = this.GetComponentInChildren<ParticleSystem>();
        _MeshRenderer = this.GetComponentInChildren<MeshRenderer>();
    } 

    protected override IEnumerator PlayTask() {
        var duration = Vector3.Distance(From, To) / Speed;
        var timeLeft = duration;

        _MeshRenderer.enabled = true;

        Debug.DrawLine(From, To, Color.cyan, duration);
        this.transform.position = From;
        this.transform.LookAt(To, Vector3.up);

        while (timeLeft > 0) {
            this.transform.position = Vector3.Lerp(From, To, 1 - timeLeft / duration);
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        this.transform.position = To;
        _MeshRenderer.enabled = false;
        _ParticleSystem.Play(true);

        yield return new WaitUntil(() => _ParticleSystem.isPlaying);
        yield return new WaitUntil(() => !_ParticleSystem.isPlaying);
        this.gameObject.SetActive(false);
    }
}
