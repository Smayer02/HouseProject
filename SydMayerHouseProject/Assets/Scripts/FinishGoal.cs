using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGoal : MonoBehaviour
{
    public Button scoreBox;
    public Text scoreText;
    public KeyCollect labKey;
    public GuiElements scores;
    public Dropdown scoreDrop;
    public bool finished = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "testing";
        scores.GetScores();
        scoreText.gameObject.SetActive(false);
        scoreDrop.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collide) {
        if (labKey.isCollected && collide.gameObject.tag == "Player") {
            finished = true;
            Time.timeScale = 0;
            scoreBox.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            scores.GetScores();
            scores.SubmitLeaderboard();
            scores.isRunning = false;
        }
    }
}
