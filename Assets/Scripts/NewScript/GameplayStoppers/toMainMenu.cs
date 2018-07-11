using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void OnMouseDown(){
		SceneManager.LoadSceneAsync ("mainMenu");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
