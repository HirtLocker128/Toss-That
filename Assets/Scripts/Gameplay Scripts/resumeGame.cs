using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resumeGame : MonoBehaviour {
	public MainGameLoop script;
	// Use this for initialization
	void Start () {
		
	}
	void OnMouseDown(){
		script.ContinueGame ();
		Destroy (GameObject.FindWithTag ("pauseCanvas"));
	}
	// Update is called once per frame
	void Update () {
		
	}
}
