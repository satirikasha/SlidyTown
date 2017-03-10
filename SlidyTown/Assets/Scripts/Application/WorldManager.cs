using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class WorldManager {

    public static string CurrentWorld {
        get {
            return WorldObjectProvider.CurrentWorld;
        }
    }

    public static List<string> UnlockedWorlds {
        get {
            return SaveManager.Data.UnlockedWorlds;
        }
    }
    public static event Action<string> OnWorldUnlocked;

    public static void UnlockWorld(string name) {
        SaveManager.Data.UnlockedWorlds.Add(name);

        if (OnWorldUnlocked != null)
            OnWorldUnlocked(name);

        Analytics.CustomEvent("WorldUnlocked", new Dictionary<string, object> {
            { "world", name }
        });
    }
}
