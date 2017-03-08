using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualEffectsManager : InstancedBehaviour<VisualEffectsManager> {

    private Dictionary<string, VisualEffect> _ResourcesCache = new Dictionary<string, VisualEffect>();
    private Dictionary<string, List<VisualEffect>> _EffectsCache = new Dictionary<string, List<VisualEffect>>();

    public void Register(VisualEffect effect) {
        if (!_EffectsCache.ContainsKey(effect.name)) {
            _EffectsCache.Add(effect.name, new List<VisualEffect>());
        }
        _EffectsCache[effect.name].Add(effect);
    }

    public void Unregister(VisualEffect effect) {
        _EffectsCache[effect.name].Remove(effect);
    }

    public VisualEffect GetEffect(string name) {
        return GetEffect<VisualEffect>(name);
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

    private VisualEffect GetEffectResource(string name) {
        if (!_ResourcesCache.ContainsKey(name)) {
            _ResourcesCache.Add(name, WorldObjectProvider.GetWorldObject<GameObject>(name).GetComponent<VisualEffect>());
        }
        return _ResourcesCache[name];
    }

    private T GetEffectResource<T>(string name) where T : VisualEffect {
        return GetEffectResource(name) as T;
    }
}
