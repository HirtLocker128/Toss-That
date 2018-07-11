using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubeScale : MonoBehaviour {
	public int meltTime = 60;
	public float meltingProportion = 0.999f;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		// sets currentScale to compare with new scale

		float curentScaleX = gameObject.transform.localScale.x;
		float curentScaleY = gameObject.transform.localScale.y;
		float curentScaleZ = gameObject.transform.localScale.z;

		Vector3 scale = new Vector3 (curentScaleX, curentScaleY, curentScaleZ);

       

        // changes size based on the current scale and public input 
        // melting proportion. The lower the faster.

        gameObject.transform.localScale = (scale * meltingProportion);

		// timeScale is input to check the time it passed

		// delta time is used to only check for real time not frame second.



		// when it hits the time it destroys the object. 



	}
}