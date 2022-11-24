using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu]
public class SkinsDatabase : ScriptableObject
{
    public Skin[] skins;

    public int sknsCount{
        get {
            return skins.Length;
        }
    }

    public Skin GetSkin(int index) {
        return skins[index];
    }


}

[System.Serializable]
public class Skin {
    public int ID;
    public GameObject skin;
    public string Name;
    public int cost;
    public int isBought;

    // private void Start() {
    //     isBought = PlayerPrefs.GetInt(Name);
    // }
}