using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.Networking;

public class ShopScript : MonoBehaviour
{
    public GameObject skins;

    public SkinsDatabase skinDB;
    public Button playButton;
    public Text nameText;
    public Text CostText;
    public Text skinstext;

    public int highScore;
    public Text highScoreText;

    public int index;
    public int money;
    public Text moneyText;

    [DllImport("__Internal")]
    private static extern void Hello();
    [DllImport("__Internal")]
    private static extern void GetData();

    public Text playerName;
    public RawImage playerImage;

    bool isData;
    // Start is called before the first frame update
    private void Start() {
        if(isData) {
            GetData();
            isData = true;
        }
        isData = true;
        UpdateSkin();
    }

    // Update is called once per frame
    void Update()
    {
        if(isData) {
            GetData();
            isData = false;
        }
        highScoreText.text = "HighScore: " + Database.Instance.data.highScore.ToString();
        moneyText.text = "Money: " + Database.Instance.data.moneys.ToString();

        skinstext.text = Database.Instance.skins[0].ToString() + " " + Database.Instance.skins[1].ToString() + " " + Database.Instance.skins[2].ToString();

        // if(Input.GetKeyDown(KeyCode.Space)) {
        //     PlayerPrefs.DeleteAll();
        //     for(int i = 1; i < skinDB.skins.Length; i++) {
        //         PlayerPrefs.SetInt(skinDB.GetSkin(i).Name, 0);
        //     }
        // }
    }
    
    public void Left() {
        Database.Instance.LoadSkins();
        index = Database.Instance.data.skinIndex;
        index--;
        if(index < 0) index = skinDB.sknsCount-1;

        Database.Instance.data.skinIndex = index;

        UpdateSkin();
    }

    public void Riht() {
        Database.Instance.LoadSkins();
        index = Database.Instance.data.skinIndex;
        index++;
        if(index >= skinDB.sknsCount) index = 0;

        Database.Instance.data.skinIndex = index;

        UpdateSkin();
    }

    public void UpdateSkin() {
        Database.Instance.LoadSkins();
        Skin skin = skinDB.GetSkin(Database.Instance.data.skinIndex);
        nameText.text = skin.Name;
        CostText.text = "Cost: " + skin.cost.ToString();

        for(int i = 0; i < skins.transform.childCount; i++) {
            if(i != Database.Instance.data.skinIndex) skins.transform.GetChild(i).gameObject.SetActive(false);
            else skins.transform.GetChild(Database.Instance.data.skinIndex).gameObject.SetActive(true);
        }

        if(Database.Instance.skins[Database.Instance.data.skinIndex] != 1) playButton.interactable = false;
        else playButton.interactable = true;
    }

    public void Buy(){
        Database.Instance.LoadSkins();
        Skin skin = skinDB.GetSkin(index);
        if(Database.Instance.skins[index] != 1 && Database.Instance.data.moneys > skin.cost) {
            Database.Instance.data.moneys -= skin.cost;
            Database.Instance.skins[index] = 1;
            skinDB.skins[index].isBought = 1;

            Database.Instance.SaveSkin();
            Database.Instance.Save();

            if(!playButton.interactable) playButton.interactable = true;
        }
        else {
            Hello();
        }
    }

    public void GetName(string name) {
        playerName.text = name;
    }
    public void GetPhoto(string url) {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string url) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) Debug.Log("Connection Error!");
        else playerImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}