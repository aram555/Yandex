using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{
    public GameObject skins;

    public SkinsDatabase skinDB;
    public int index;
    private void Awake() {
        index = Database.Instance.data.skinIndex;
    }
    // Start is called before the first frame update
    void Start()
    {
        index = Database.Instance.data.skinIndex;

        UpdateSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSkin() {
        Skin skin = skinDB.GetSkin(index);

        for(int i = 0; i < skins.transform.childCount; i++) {
            if(i != Database.Instance.data.skinIndex) skins.transform.GetChild(i).gameObject.SetActive(false);
            else skins.transform.GetChild(Database.Instance.data.skinIndex).gameObject.SetActive(true);
        }
    }
}
