using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateScript : MonoBehaviour
{
    [Header("Color")]
    public string color;
    [Header("Score and Money")]
    public int score;
    public int money;
    public Text scoreText;
    public Text moneyText;
    [Header("Game Manager")]
    public GameObject gameManager;
    public GameManager managerScript;
    [Header("Particles")]
    public GameObject particle;
    public Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();

        score = Random.Range(50, 200);
        money = Random.Range(1, 20);

        managerScript.spawnedGates.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        moneyText.text = money.ToString();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Ball")) {
            BallScript ball = other.gameObject.GetComponent<BallScript>();
            if(ball.color.Equals(color)) {
                
                Database.Instance.data.score += score;
                Database.Instance.data.moneys += money;
                
                managerScript.timer += Random.Range(1, 3);
                
                managerScript.spawnedGates.Remove(this.gameObject);
                Destroy(this.gameObject);
                ball.transform.position = ball.ballPoints[ball.i].position;
                
                GameObject Particle = (GameObject) Instantiate(particle, transform.position, Quaternion.identity);
                ParticleSystem.MainModule Part = Particle.GetComponent<ParticleSystem>().main;
                ParticleSystem.TrailModule Trail = Particle.GetComponent<ParticleSystem>().trails;

                int i = Random.Range(0, colors.Length-1);
                Part.startColor = colors[i];
                Trail.colorOverLifetime = colors[i];
                
            }
            else {
                return;
            }
        }
    }
}
