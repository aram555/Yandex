using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    [Header("Balls and Positions")]
    public Transform[] ballPoints;
    public int ballCount;
    public int i;
    [Header("Color")]
    public string color;
    [Header("Trail")]
    public GameObject[] trail;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        i = ballCount;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            isActive = false;
            i++;
            if(i > ballPoints.Length-1) i = 0;
            transform.position = ballPoints[i].position;
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            isActive = false;
            i--;
            if(i < 0) i = ballPoints.Length-1;
            transform.position = ballPoints[i].position;
        }

        if(isActive) {
            for(int b = 0; b < trail.Length; b++) {
                trail[b].SetActive(true);
            }
        }
        else {
            for(int b = 0; b < trail.Length; b++) {
                trail[b].SetActive(false);
            }
        }
    }
}
