using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[ExecuteInEditMode]
[DefaultExecutionOrder(0)]
public static class WorldObjectProvider {

    public const string DefaultWorld = "FastAndFurious";

    public const string ResourcesWorldsPath = "World";
    public const string AssetsWorldsPath = "Assets/Resources/" + ResourcesWorldsPath;
    public const string CurrentWorldKey = "CurrentWorld";

    public static event Action OnWorldChanged;
    public static event Action OnWorldApplied;
    public static event Action AfterWorldApplied;

    public static string CurrentWorld {
        get {
            if (String.IsNullOrEmpty(_CurrentWorld)){
                if (PlayerPrefs.HasKey(CurrentWorldKey)) {
                    _CurrentWorld = PlayerPrefs.GetString(CurrentWorldKey);
                }
                else {
                    _CurrentWorld = DefaultWorld;
                    PlayerPrefs.SetString(CurrentWorldKey, _CurrentWorld);
                }
            }
            return _CurrentWorld;
        }
        set {
            if (_CurrentWorld != value) {
                _CurrentWorld = value;
                PlayerPrefs.SetString(CurrentWorldKey, _CurrentWorld);
                _CurrentWorldData = null;               
                _WorldDirty = true;
                if (OnWorldChanged != null)
                    OnWorldChanged();
            }
        }
    }
    [WorldSelector]
    private static string _CurrentWorld;
    private static bool _WorldDirty;

    public static string CurrentWorldPath {
        get {
            return ResourcesWorldsPath + "/" + CurrentWorld;
        }
    }

    public static WorldData CurrentWorldData {
        get{
            if(_CurrentWorldData == null)
                _CurrentWorldData = WorldConfig.Instance.Data.FirstOrDefault(_ => _.Name == _CurrentWorld);
            return _CurrentWorldData;
        }
    }
    private static WorldData _CurrentWorldData;

    private static Dictionary<string, Object[]> _WorldCache = new Dictionary<string, Object[]>();

    public static void ApplyWorldChanges() {
        if (_WorldDirty) {
            if (OnWorldApplied != null)
                OnWorldApplied();
            if (AfterWorldApplied != null)
                AfterWorldApplied();
            _WorldDirty = false;
        }
    }

    public static Object GetWorldObject(string world, string name) {
        PrepareWorld(world);
        return _WorldCache[world].FirstOrDefault(_ => _.name == name);
    }

    public static T GetWorldObject<T>(string world, string name) where T : Object {
        return GetWorldObject(world, name) as T;
    }

    public static Object GetWorldObject(string name) {
        return GetWorldObject(CurrentWorld, name);
    }

    public static T GetWorldObject<T>(string name) where T : Object {
        return GetWorldObject(name) as T;
    }

    public static void PrepareWorld(string world) {
        if (!_WorldCache.ContainsKey(world)) {
            _WorldCache.Add(world, Resources.LoadAll(ResourcesWorldsPath + "/" + world));
        }
    }

#if UNITY_EDITOR
    public static void RefreshWorld(string world) {
        _WorldCache[world] = Resources.LoadAll(ResourcesWorldsPath + "/" + world);
    }
#endif
}
