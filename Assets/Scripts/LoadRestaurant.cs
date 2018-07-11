using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRestaurant : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnMouseDown(){
		SceneManager.LoadSceneAsync("RestaurantScene");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
