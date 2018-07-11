using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadKitchen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void OnMouseDown(){
		SceneManager.LoadSceneAsync ("KitchenScene");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
