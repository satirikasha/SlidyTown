using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualEffectsManager : InstancedBehaviour<VisualEffectsManager> {

    private static Dictionary<string, VisualEffect> _ResourcesCache = new Dictionary<string, VisualEffect>();
    private static Dictionary<string, List<VisualEffect>> _EffectsCache = new Dictionary<string, List<VisualEffect>>();

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Register(VisualEffect effect) {
        if (!_EffectsCache.ContainsKey(effect.name)) {
            _EffectsCache.Add(effect.name, new List<VisualEffect>());
        }
        _EffectsCache[effect.name].Add(effect);
    }

    public void Unregister(VisualEffect effect) {
        _EffectsCache[this.name].Remove(effect);
    }

    public T GetEffect<T>(string name) where T : VisualEffect {
        T result = null;
        if (_EffectsCache.ContainsKey(name)) {
            result = _EffectsCache[name].FirstOrDefault(_ => !_.gameObject.activeSelf) as T;
        }
        if (result == null) {
            result = Instantiate(GetEffectResource<T>(name), this.transform, false);
            result.name = name;
            result.gameObject.SetActive(false);
            Register(result);
        }
        return result;
    }

    private static VisualEffect GetEffectResource(string name) {
        if (!_ResourcesCache.ContainsKey(name)) {
            _ResourcesCache.Add(name, WorldObjectProvider.GetWorldObject<GameObject>(name).GetComponent<VisualEffect>());
        }
        return _ResourcesCache[name];
    }

    private static T GetEffectResource<T>(string name) where T : VisualEffect {
        return GetEffectResource(name) as T;
    }
}
