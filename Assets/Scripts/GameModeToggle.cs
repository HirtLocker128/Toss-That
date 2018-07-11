using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeToggle : MonoBehaviour {


    //this script will hold a bool that will decide the mode of the game


    public static GameModeToggle control;

    public bool gameMode;

	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(gameObject);

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
