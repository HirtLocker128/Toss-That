using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAutoControl : MonoBehaviour {

    public GameObject remaining;
    public GameObject timer;

	// Use this for initialization
	void Awake () {
        
        if (GameObject.Find("GameModeToggle").GetComponent<GameModeToggle>().gameMode)
        {
            remaining.SetActive(false);
            timer.SetActive(true);
            print("Timer Worked");
        }

        if (!GameObject.Find("GameModeToggle").GetComponent<GameModeToggle>().gameMode)
        {
            remaining.SetActive(true);
            timer.SetActive(false);
            print("Remaining Worked");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
