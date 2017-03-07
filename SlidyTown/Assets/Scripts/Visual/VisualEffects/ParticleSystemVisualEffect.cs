using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemVisualEffect : VisualEffect {

    private ParticleSystem _ParticleSystem;

    void Awake() {
        _ParticleSystem = this.GetComponent<ParticleSystem>();
    } 

    protected override IEnumerator PlayTask() {
        _ParticleSystem.Play(true);
        yield return new WaitUntil(() => _ParticleSystem.isPlaying);
        yield return new WaitUntil(() => !_ParticleSystem.isPlaying);
        this.gameObject.SetActive(false);
    }
}
