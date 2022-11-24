using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data {
    public int skinIndex;
    public int moneys;
    public int score;
    public int highScore;
    public int skin0;
    public int skin1;
    public int skin2;
}

public class Database : MonoBehaviour
{
    public static Database Instance;
    public Data data;
    public int[] skins;

    public Text playerData;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    bool load;

    private void Start() {
        load = true;
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        else {
            Instance = this;
            LoadExtern();
            LoadSkins();
        }
    }

    private void Awake() {
        load = true;
        LoadExtern();
        LoadSkins();
    }

    private void Update() {
        if(load) {
            LoadExtern();
            LoadSkins();
            load = false;
        }
        playerData.text = data.moneys + " Moneys\n" + data.score + " Score\n" + data.highScore + " HighScore\n" + data.skinIndex + " SkinIndex\n" + data.skin0 + " Skin0\n" + data.skin1 + " Skin1\n" + data.skin2 + " Skin2\n";
    }

    public void SaveSkin() {
        data.skin0 = skins[0];
        data.skin1 = skins[1];
        data.skin2 = skins[2];
    }

    public void LoadSkins() {
        skins = new int[3] {data.skin0, data.skin1, data.skin2};
    }

    public void Save() {
        string jsonString = JsonUtility.ToJson(data);
        SaveExtern(jsonString);
        playerData.text = data.moneys + " Moneys\n" + data.score + " Score\n" + data.highScore + " HighScore\n" + data.skinIndex + " SkinIndex\n" + data.skin0 + " Skin0\n" + data.skin1 + " Skin1\n" + data.skin2 + " Skin2\n";
    }

    public void SetPlayerInfo(string value) {
        data = JsonUtility.FromJson<Data>(value);
        playerData.text = data.moneys + " Moneys\n" + data.score + " Score\n" + data.highScore + " HighScore\n" + data.skinIndex + " SkinIndex\n" + data.skin0 + " Skin0\n" + data.skin1 + " Skin1\n" + data.skin2 + " Skin2\n";
    }
}
