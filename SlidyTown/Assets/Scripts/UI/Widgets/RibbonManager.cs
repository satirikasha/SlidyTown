using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public static class RibbonManager {

    public static Dictionary<string, RibbonType> Highlighted = new Dictionary<string, RibbonType>();

    [RuntimeInitializeOnLoadMethod]
    private static void Init() {
        CurrencyManager.OnCoinsAdded += CheckUnlock;
    }


    private static void CheckUnlock(int deltaCoins) {
        var unlocked = WorldConfig.Instance.Data
            .Where(_ => !WorldManager.UnlockedWorlds.Contains(_.Name) && _.Price <= CurrencyManager.Coins && _.Price > CurrencyManager.Coins - deltaCoins)
            .Select(_ => _.Name);

        foreach (var unlock in unlocked) {
            Highlighted[unlock] = RibbonType.Unlock;
        }
    }

    public static void OnRibbonObserved(string world) {
        Highlighted.Remove(world);
    }
}
