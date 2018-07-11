using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public MainGameLoop script;
	public bool paused;
	public Text pauseText;
	// Use this for initialization
	void Start () {
		paused = false;
		pauseText = GetComponent<Text> ();
		pauseText.text = "Pause";
	}
	void OnMouseDown(){
		if (paused == true) {
			script.ContinueGame ();
			pauseText.text = "Pause";
			paused = false;
			return;
		}
		if (paused == false) {
			script.PauseGame ();
			pauseText.text = "Resume";
			paused = true;
			return;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
