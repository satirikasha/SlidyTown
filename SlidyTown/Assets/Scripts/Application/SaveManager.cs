using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager {

    public static SaveData Data {
        get {
            if (_Data == null) {
                Load();
            }
            return _Data;
        }
    }
    private static SaveData _Data;

    private static string Path {
        get {
            return Application.persistentDataPath + "/save.dat";
        }
    }

    public static void Save() {
        var bf = new BinaryFormatter();
        using (var fs = File.Open(Path, FileMode.OpenOrCreate)) {
            bf.Serialize(fs, Data);
            fs.Close();
        }
    }

    public static void Load() {
        if (File.Exists(Path)) {
            try {
                var bf = new BinaryFormatter();
                using (var fs = File.Open(Path, FileMode.Open)) {
                    _Data = (SaveData)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch {
                _Data = GetDefaultData();
            }
        }
        else {
            _Data = GetDefaultData();
        }
    }

    private static SaveData GetDefaultData() {
        return new SaveData() {
            Version = 0,
            Coins = 0,
            MaxPoints = 0,
            UnlockedWorlds = new List<string>() { "Farm" }
        };
    }
}

[Serializable]
public class SaveData {
    public int Version;
    public int Coins;
    public int MaxPoints;
    public List<string> UnlockedWorlds;
}
