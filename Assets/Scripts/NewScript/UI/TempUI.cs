using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour {

    // Use this for initialization
    private int tempVal;
    private Text temp;
    void Awake()
    {
        temp = GetComponent<Text>();
        tempVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (GameObject.Find ("Table")) {
			temp.text = "Temperature: " + GameObject.Find ("Table").GetComponent<Melter> ().getTemperature ().ToString () + " degrees";
		} else
			Destroy (this);
    }
}
