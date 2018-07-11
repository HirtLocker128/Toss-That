using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    // Use this for initialization
    public int scoreVal = 0;
    public Text score1;
    
	void Awake () {
		score1 = GetComponent<Text> ();
        //scoreVal = 0;
	}
	
	// Update is called once per frame
	void Update () {
        score1.text = scoreVal.ToString();;
        
	}

    public void onePoint()
    {
        scoreVal++;
    }
}
