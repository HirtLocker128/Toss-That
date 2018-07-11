using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJDiscRotate : MonoBehaviour {

//	private int[] speed = { 10, -10, 20, -20, 15, -15, 25, -25, 40, -40, 35, -35, 30, -35 };
	private int[] speed = { 20, -20, 25, -25, 40, -40, 35, -35, 30, -35 };
	private double time = 5.0f;
	private double timer = 0.0;
	private int new_speed = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		timer += Time.deltaTime;
		if (timer > time) {
			new_speed = speed [Random.Range (0, speed.Length)];
			transform.Rotate (0, new_speed * Time.deltaTime, 0);
			timer = 0.0;
		} else {
			transform.Rotate (0, new_speed * Time.deltaTime, 0);
		}
	}
}
