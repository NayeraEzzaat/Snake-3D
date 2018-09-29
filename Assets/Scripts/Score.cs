using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public GameObject gameOver;
    private float totalTime;
    private float oneSecond;
    private float totalScore;
    [HideInInspector]
    public float bonusScore;
    [HideInInspector]
    public float timeScore;
    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool isFalling;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public int scoreRate;
    

    void Start() {
        bonusScore = 0;
        totalScore = 0;
        totalTime = 0;
        oneSecond = 0;
        timeScore = 0;
        scoreRate = (int)(GetComponentInParent<Variables>().snakeSpeed);
        isMoving = false;
        isFalling = false;
        isDead = false;
    }

    void Update() {
        scoreRate = (int)(GetComponentInParent<Variables>().snakeSpeed);

        if (isFalling || isDead) {
            gameOver.SetActive(true);
        } else if (isMoving) {
            totalTime += Time.deltaTime;
            oneSecond += Time.deltaTime;
        }
        if (Mathf.RoundToInt(oneSecond % 60f) == 1) {
            UpdateLevelTimer(totalTime);
            oneSecond -= 1;
        }
    }


    public void UpdateLevelTimer(float totalSeconds) {
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);
        timeScore += scoreRate;
        totalScore = timeScore + bonusScore;
        GetComponent<Text>().text = "" + totalScore ;
       
    }
}
