using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    int score;
    int highScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int Level) {
        score = Database.Instance.data.score;
        highScore = Database.Instance.data.highScore;
        
        SceneManager.LoadScene(Level);
        
        if(Level == 0) {
            if(score > highScore) {
                Database.Instance.data.highScore = score;
            }
        }
        Database.Instance.data.score = 0;
        Database.Instance.Save();
        Database.Instance.SaveSkin();
    }

    public void Restart()
    {
        score = Database.Instance.data.score;
        highScore = Database.Instance.data.highScore;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        if(score > highScore) {
            Database.Instance.data.highScore = score;
        }
        Database.Instance.data.score = 0;
        Database.Instance.Save();
    }

    public void Exit() {
        Application.Quit();
    }

    private void OnApplicationQuit() {
        Database.Instance.Save();
    }
}
