using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectNext : MonoBehaviour {
	public GameObject kitchen;
	public GameObject dj;
	public GameObject bar;
	public GameObject restaurant;
	public int selector; 

	// Use this for initialization
	void Start () {
		selector = 0;
		kitchen.SetActive (true);
		dj.SetActive (false);
		bar.SetActive (false);
		restaurant.SetActive (false);

	}
		
	void OnMouseDown(){
		if (selector == 0) {
			selector = 1;
			return;
		}
		if (selector == 1) {
			selector = 2;
			return;
		}
		if (selector == 2) {
			selector = 3;
			return;
		}
		if (selector == 3) {
			selector = 0;
			return;
		 }
	}

	// Update is called once per frame
	void Update () {
		if (selector == 0) {
			kitchen.SetActive (true);
			dj.SetActive (false);
			bar.SetActive (false);
			restaurant.SetActive (false);

	}
		if (selector == 1) {
			kitchen.SetActive (false);
			dj.SetActive (true);
			bar.SetActive (false);
			restaurant.SetActive (false);

		}
		if (selector == 2) {
			kitchen.SetActive (false);
			dj.SetActive (false);
			bar.SetActive (true);
			restaurant.SetActive (false);

		}
		if (selector == 3) {
			kitchen.SetActive (false);
			dj.SetActive (false);
			bar.SetActive (false);
			restaurant.SetActive (true);
				
		}
}
}