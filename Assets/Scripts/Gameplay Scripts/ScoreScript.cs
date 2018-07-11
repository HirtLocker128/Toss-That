using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    // Use this for initialization
    public Text scoreText;
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + MainGameLoop.score.ToString();
	}
}
