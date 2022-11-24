using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Gates and Spawn points")]
    public List<GameObject> spawnedGates = new List<GameObject>();
    public GameObject[] gates;
    public Transform[] sapwnPoints;
    [Header("Score and Money")]
    public int score;
    public int money;
    public int highScore;
    public Text highScoreText;
    public Text scoreText;
    public Text scoreTextTwo;
    public Text moneytext;
    
    [Header("Timer")]
    public float timer;
    public Text timerText;
    public GameObject retraet;
    [Header("Spawn Objects")]
    public Material grassMaterial;
    public GameObject[] greenEnviroment;
    public GameObject[] redEnviroment;
    public GameObject[] violetEnviroment;
    public GameObject[] blueEnviroment;
    public Color[] enviromentColor;
    [Header("Spawn Positions")]
    public Vector3 center;
    public Vector3 size;

    Camera cam;
    List<GameObject[]> enviroment;
    void Start()
    {

        cam = Camera.main;
        Time.timeScale = 1;
        enviroment = new List<GameObject[]>() {greenEnviroment, redEnviroment, violetEnviroment, blueEnviroment};
        SpawnEnviroment();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0) {
            retraet.SetActive(true);
            Time.timeScale = 0;
            Database.Instance.Save();
        }
        if(spawnedGates.Count <= 0) SpawnGates();

        scoreText.text = "Score: " + Database.Instance.data.score.ToString();
        moneytext.text = "Moneys: " + Database.Instance.data.moneys.ToString();
        scoreTextTwo.text = "Score: " + Database.Instance.data.score.ToString();
        highScoreText.text = "HighScore: " + Database.Instance.data.highScore.ToString();

        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.RoundToInt(timer).ToString();

        if(Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if(hit.collider.CompareTag("Ball")) {
                    BallScript ball = hit.collider.GetComponent<BallScript>();
                    //hit.collider.gameObject.transform.Translate(transform.forward * 50 * Time.deltaTime);
                    ball.transform.Translate(transform.forward * 50 * Time.deltaTime);
                    ball.isActive = true;
                }
            }
        }
    }

    private void SpawnEnviroment() {
        int envir = Random.Range(0, enviroment.ToArray().Length);
        GameObject[] Enviroment = enviroment[envir];
        for(int i = 0; i < enviroment[envir].Length; i++) {
            Vector3 newPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(Enviroment[i], newPos, Quaternion.Euler(0, Random.Range(0, 180), 0));
        }
        grassMaterial.color = enviromentColor[envir];
    }

    private void SpawnGates() {
        for(int i = 0; i < gates.Length; i++) {
            int j = Random.Range(0, gates.Length);

            var temp = gates[j];
            gates[j] = gates[i];
            gates[i] = temp;
        }

        for(int i = 0; i < gates.Length; i++) {
            Instantiate(gates[i], sapwnPoints[i].position, Quaternion.identity);
        }
    }

}
