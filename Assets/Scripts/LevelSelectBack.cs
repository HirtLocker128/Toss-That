using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBack : MonoBehaviour {
	public LevelSelectNext script;
 	// Use this for initialization
	void Start () {
	}

	void OnMouseDown(){
		if (script.selector == 0) {
			script.selector = 3;
			return;
		}
		if (script.selector == 1) {
			script.selector = 0;
			return;
		}
		if (script.selector == 2) {
			script.selector = 1;
			return;
		}
		if (script.selector == 3) {
			script.selector = 2;
			return;
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
