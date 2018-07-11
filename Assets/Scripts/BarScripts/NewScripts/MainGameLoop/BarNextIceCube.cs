using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is used for Ice Collision Behavior
All ice cubes are linked to this script.

However, this script is ONLY enabled when the ice cube
is finished with toss mode. Otherwise, it is disabled.

This Script's Behavior:
Check how to update the next ice cube.
*/
public class BarNextIceCube : MonoBehaviour {
    /*
    Private Substate Variables
    */
    private enum state {success, fail, handled , none, pause, paused, unpause };
    private state substate;
    private state savedState;
    /*
    Private Variables
    */
    private GameObject nextGameObject;
    private int currIceIndex;
    
    

    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function only activates when the Scene is loaded, even if the script is disabled.
    If the scene is reloaded within itself, we need to make our own function: RepeatStart.

    This function initializes all member variables.
    */
    void Awake () {
        substate = BarNextIceCube.state.none;
        nextGameObject = null;
        currIceIndex = int.Parse(gameObject.name.Substring(4, gameObject.name.Length - 4));
        //needed to prevent OnEnable from activating when the script is attached to the
        //next ice obj
        gameObject.GetComponent<BarNextIceCube>().enabled = false;
    }



    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function activates every 16.67 milliseconds (for 60 frames-per-second).

    Here, the Update function checks what this script's substate is, then takes the
    appopriate action. Once this script's work is done, it transfers control to Tosser.
    */
    void Update () {
        if (substate == BarNextIceCube.state.handled)
        {
            //prepare to remove all the scripts from the currently tossed ice cube, and
            //move it to the next ice cube.
			nextGameObject.AddComponent<BarTosser>();
            nextGameObject.AddComponent<BarIceCollider>();
            nextGameObject.AddComponent<BarNextIceCube>();
            
            //no logic should come after this code block and after the if-elseif's here.
			Destroy(gameObject.GetComponent<BarTosser>());
            Destroy(gameObject.GetComponent<BarIceCollider>());

            substate = BarNextIceCube.state.none;
            nextGameObject = null;
            //We destroy this script
            Destroy(gameObject.GetComponent<BarNextIceCube>());
        }
        else if (substate == BarNextIceCube.state.success)
        {
            //add to the score.

            //change to the next ice cube on the plate.
            switchIceCubes();
            //now that the next ice cube is handled, set the substate.
            substate = BarNextIceCube.state.handled;
        }
        else if (substate == BarNextIceCube.state.fail)
        {
            //send signal to ChancesLeft

            //change to the next ice cube on the plate.
            switchIceCubes();
            //now that the next ice cube is handled, set the substate.
            substate = BarNextIceCube.state.handled;
        }
        else if (substate == BarNextIceCube.state.none)
        {

        }
        else if (substate == BarNextIceCube.state.pause)
        {
            
            substate = BarNextIceCube.state.paused;
        }
        else if (substate == BarNextIceCube.state.paused)
        {

        }
        else if (substate == BarNextIceCube.state.unpause)
        {
            substate = savedState;
        }
    }

    /*
    This is a Unity Event Handler defined by MonoBehaviour.
    It activates whenever this script is enabled, when the tossed 
    ice cube needs to be moved out.
    */
    void onEnable()
    {
        //Don't set the substate to state.none because
        //state.success or state.fail would be set by IceCollider.
    }
    /*
    This is a Unity Event Handler defined by MonoBehaviour.
    It activates whenever this script is destroyed,
    */
    void OnDestroy()
    {
        
    }

    /*
    This function and onEnable is activated when IceCollider enables
    this script. The initial substate while this function is active is none.
    Then once the work is done, the substate is set to handledIceCube
    */
    public void successThrow()
    {
        GameObject.Find("Score").GetComponentInChildren<ScoreUI>().onePoint();
        substate = BarNextIceCube.state.success;
    }
    public void failThrow()
    {
        ChancesLeft.decrementChance();
        substate = BarNextIceCube.state.fail;
    }
    /*
    This function is usd to transition between ice cubes after one is successfully
    or unsuccessfully tossed.
    */
    void switchIceCubes()
    {
		if (GameObject.Find ("Table")) {
			//This makes the tossed ice cube go behind the camera.
			Vector3 outOfView = GameObject.Find ("Table").GetComponent<BarCubeRestorer> ().getOutOfView (currIceIndex);
			gameObject.transform.position = outOfView;
			//This sets the tossed ice cube to the default orientation.
			gameObject.transform.rotation = new Quaternion (0, 0, 0, 1);
			//This negates gravity so the ice cube stays in one place.
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			//This negates prior movement so the ice cube doesn't float away (via Newton's 1st Law)
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);


			//calls a helper to choose the next ice cube. If all ice cubes are used up, the
			//plate of ice cubes is reset.
			increaseIndex ();

			//We change the focused ice cube (i.e. ice cube to toss) from the tossed
			//one to the next one on the plate with GameObject.Find.
			nextGameObject = GameObject.Find ("Cube" + currIceIndex.ToString ());
			//GameObject.Find ("pauseButton").GetComponent<BarPaused> ().updateIceCube (nextGameObject);
			GameObject.Find ("Table").GetComponent<BarMelter> ().updateIceCube (nextGameObject);

			//We set the new ice cube to the tossable position and orientation.
			Vector3 toss = GameObject.Find ("Table").GetComponent<BarCubeRestorer> ().getTossPos ();
			nextGameObject.transform.position = toss;

			nextGameObject.transform.rotation = GameObject.Find ("Table").GetComponent<BarCubeRestorer> ().getTossOri ();
			//We still negate gravity and movement. They will activate only when our finger
			//motion tosses the ice cube.
			nextGameObject.GetComponent<Rigidbody> ().isKinematic = true;
			nextGameObject.GetComponent<Rigidbody> ().useGravity = false;
			nextGameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		} else
			Destroy (this);
    }

    /*
    This function increases the currIceIndex or refills the place
    */
    void increaseIndex()
    {
        //but if currIceIndex is 14, get a new plate of ice
        //and reset currIceIndex to 0.
        if (currIceIndex == 14)
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject iObj = GameObject.Find("Cube" + i.ToString());
                iObj.transform.position = GameObject.Find("Table").GetComponent<BarCubeRestorer>().getPlatePos(i);
                iObj.transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            //used in a separate loop 
            for (int i = 0; i < 15; i++)
            {
                GameObject iObj = GameObject.Find("Cube" + i.ToString());
                iObj.GetComponent<Rigidbody>().isKinematic = false;
                iObj.GetComponent<Rigidbody>().useGravity = true;
            }
            GameObject.Find("Table").GetComponent<BarMelter>().temperatureIncreases();
            currIceIndex = 0;
        }
        else
        {
            currIceIndex++;
        }
    }

    public void pauseGame()
    {
        savedState = substate;
        substate = BarNextIceCube.state.pause;
    }
    public void unpauseGame()
    {
        substate = BarNextIceCube.state.unpause;
    }
}
