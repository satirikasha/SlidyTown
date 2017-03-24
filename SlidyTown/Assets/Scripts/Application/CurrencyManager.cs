using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyManager {

    public static int Coins {
        get {
            return SaveManager.Data.Coins;
        }
    }
    public static event Action<int> OnCoinsAdded;
    public static event Action<int> OnCoinsSpent;

    public static void AddCoins(int ammount) {
        SaveManager.Data.Coins += ammount;
        SaveManager.Save();
        if (OnCoinsAdded != null)
            OnCoinsAdded(ammount);
    }

    public static void SpendCoins(int ammount) {
        SaveManager.Data.Coins -= ammount;
        if (OnCoinsSpent != null)
            OnCoinsSpent(ammount);
    }
}
