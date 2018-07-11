using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDJScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnMouseDown(){
		SceneManager.LoadSceneAsync("DJ_Scene");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
