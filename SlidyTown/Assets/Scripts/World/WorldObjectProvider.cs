﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[ExecuteInEditMode]
[DefaultExecutionOrder(0)]
public static class WorldObjectProvider {

    public const string ResourcesWorldsPath = "World";
    public const string AssetsWorldsPath = "Assets/Resources/" + ResourcesWorldsPath;
    public const string CurrentWorldKey = "CurrentWorld";
    public const string DefaultWorld = "Farm";

    public static event Action OnWorldChanged;

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
                if (OnWorldChanged != null)
                    OnWorldChanged();
            }
        }
    }
    [WorldSelector]
    private static string _CurrentWorld;

    public static string CurrentWorldPath {
        get {
            return ResourcesWorldsPath + "/" + CurrentWorld;
        }
    }

    private static Dictionary<string, Object[]> _WorldCache = new Dictionary<string, Object[]>();

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
