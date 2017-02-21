using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[ExecuteInEditMode]
[DefaultExecutionOrder(0)]
public class WorldObjectProvider : SingletonBehaviour<WorldObjectProvider> {

    public const string ResourcesWorldsPath = "World";
    public const string AssetsWorldsPath = "Assets/Resources/" + ResourcesWorldsPath;

    public static event Action OnWorldChanged;

    public static string CurrentWorld {
        get {
            return Instance._CurrentWorld;
        }
        set {
            Instance._CurrentWorld = value;
            if (OnWorldChanged != null)
                OnWorldChanged();
        }
    }
    [WorldSelector]
    [SerializeField]
    private string _CurrentWorld;

    private static Dictionary<string, Object[]> _WorldCache = new Dictionary<string, Object[]>();

    void OnValidate() {
        if (OnWorldChanged != null)
            OnWorldChanged();
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
}
