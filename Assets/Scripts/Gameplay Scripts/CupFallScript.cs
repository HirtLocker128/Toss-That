//Imports for Unity C# Programming
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is used for Cup Tipping Behavior
The Table object is linked to this script.

This Script's Behavior:
1. Resets cup position when the scene is reset.
2. Checks if the cup tips over.
3. There is a substate here that checks if the cup tips. If so, the Update function
   activates a GameOver.

IMPROVEMENTS TODO: 
1. As I mentioned in IceCubeScript, I think we should integrate 3 scripts (this one, IceCubeScript,
   and IceCubeScale) into one script that is attached to the Table Object. That way, there would be 
   less strain on the system because 15 ice cubes won't have running scripts.
2. Replace all substates with 1 enumerator since only one substate is true while the rest are false.
*/
public class CupFallScript : MonoBehaviour {
    /*
    Static Substate Variables
    */
    private static bool cupFell;
    /*
    Private Members - used for re-initialization on 
    a Scene Change.
    */
    private static float[] cupOri = {0,0,0 };
    private static GameObject cup = null;

    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function only activates when the Scene is loaded.
    If the scene is reloaded, we need to make our own function: RepeatStart.

    This function initializes all member variables.
    */
    void Start () {
        cupFell = false;
        if (cup == null)
        {
            cup = GameObject.Find("CupRigidbody");
        }
        //save cup's original position.
        cupOri[0] = cup.transform.position.x;
        cupOri[1] = cup.transform.position.y;
        cupOri[2] = cup.transform.position.z;
    }

    /*
    This function NEEDS TO BE ACTIVATED whenever the scene is reloaded (i.e. pause + reset
    game or gameover + restart)
    The substates and UI variables are all reset, and the ice cube position is reset.
    */
    public void RepeatStart () {
        cupFell = false;
        //reset cup's position and make it stand upright with a (0,0,0,1) quaternion.
        cup.transform.position = new Vector3(cupOri[0], cupOri[1], cupOri[2]);
        Debug.Log("cup orig pos: " + cup.transform.position);
        cup.transform.rotation = new Quaternion(0, 0, 0, 1);
        Debug.Log("cup orig rot: " + cup.transform.rotation);
    }
    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function activates every 16.67 milliseconds (for 60 frames-per-second).

    Here, the Update function checks if this script's gameobject (a table) collides with
    the cup (as the cup falls on the table). If so, a game over in MainGameLoop is activated.
    */
    void Update () {
		if (cupFell)
        {
            MainGameLoop.gameEnds = true;
            //to prevent gameEnds from repeatedly being set to true.
            cupFell = false;
        }
	}
    /*
    This is a Unity Event Handler that activates whenever a GameObject
    with Collider col enters the Collider of this script's gameobject (a cup).
    */
    void OnCollisionEnter(Collision col) {
        /*
        This conditional requires some explanation. 
        col.gameObject.name refers to the name of the parent object that collided with
        the Table.
        col.collider.gameObject.name refers to the exact child of the parent object that
        collided with the Table.

        We want the game over to be thrown when the cup tips. However, CupRB (22) is the 
        collider for the bottom of the cup. We don't want the game to end if the bottom of the cup
        touches the table (because it's underwhelming). But if any other part touches the table,
        then we activate the Game Over in MainGameLoop.
        */
        if (col.gameObject.name == "CupRigidbody" && col.collider.gameObject.name != "CupRB (22)")
        {
            cupFell = true;
            return;
        }
    }
}
