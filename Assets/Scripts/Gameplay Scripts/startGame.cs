using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class startGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnMouseDown(){
		SceneManager.LoadSceneAsync("DimensionScene");
		ChancesLeft.resetChances ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
