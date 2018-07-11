//Imports for Unity C# Programming
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Import for Timer class.
using System.Timers;

/*
This class is used for Ice Cube Behavior.
Each Ice Cube Object is linked to this script.

This Script's Behavior:
1. Resets ice position when the Scene is restarted
2. When the ice cube is tossed, the collision checks occur here.
3. There are a set of states here for the Update function, seeing if the ice cube
   landed on any of them after a toss. To differentiate them from MainGameLoop, let's
   call them substates.

IMPROVEMENTS TODO: 
1. Reset ice size here, when the Scene is restarted. We should integrate IceCubeScale
   to this class, as this class already takes care of resets and cycling through the cubes.
2. Replace all substates with 1 enumerator since only one substate is true while the rest are false.
*/
public class IceCubeScript : MonoBehaviour
{
    /*
    Private Members
    */
    //For the 3 second delay if a cup fails to land in the cup.
	public static Timer timer = null;
	public static bool timerStarted = false;
    //collider within cup that checks if an ice cube passes through.
    private GameObject cupBottom = null;
    /*
    Public Static Substate Variables
    */
    public static bool collided;
    public static bool tableCollided;
    public static bool iceIsOnTable;
    /*
    Public Static UI Variables
    */
    public static int missCount;
    /*
    TODO: Document pausing and ice cube scaling
    */
    public bool gamePaused;
    // stores original size and retunrs to it 
    public Vector3 originalSize;


    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function only activates when the Scene is loaded.
    If the scene is reloaded, we need to make our own function: RepeatStart.

    This function initializes all member variables.
    */
    void Start()
    {
        //This code is repeated in RepeatStart
        missCount = 3;
        collided = false;
        tableCollided = false;
        iceIsOnTable = false;
		timerStarted = false;
        // set originalSize for initialization.
        originalSize = transform.localScale;

        //Because this function is called every time an
        //Ice Cube is made, we don't want to repeatedly
        //initialize static variables.
        if (cupBottom == null)
            cupBottom = GameObject.Find("CupCollider");
        if (timer == null)
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(endTimer);
            timer.Interval = 3000;
            timer.Stop();
        }
    }

    /*
    This function NEEDS TO BE ACTIVATED whenever the scene is reloaded (i.e. pause + reset
    game or gameover + restart)
    The substates and UI variables are all reset, and the ice cube position is reset.

    TODO: Remove i, it's uneeded.
    TODO: Add ice cube size reset here.
    */
    public void RepeatStart(int i)
    {
        //This code is repeated in Start
        missCount = 3;
        collided = false;
        tableCollided = false;
        iceIsOnTable = false;
		timerStarted = false;
        gameObject.transform.position = new Vector3(MainGameLoop.plateOri[i, 0],
                                                    MainGameLoop.plateOri[i, 1],
                                                    MainGameLoop.plateOri[i, 2]);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function activates every 16.67 milliseconds (for 60 frames-per-second).

    Here, the Update function checks if this script's gameobject (an ice cube) collides
    with anything after a toss. Then, the appropriate collision code is activated.

    IMPROVEMENTS TODO: Incorporate melting here.
    */
    void Update()
    {

        //ice cube has landed outside the cup and stayed there for 3 seconds
        if (iceIsOnTable)
        {
            switchIceCubes();
            iceIsOnTable = false;
            
            transform.localScale = originalSize;
            return;
        }
        //if ice cube lands in the cup.
        if (collided)
        {
            MainGameLoop.score++;
            switchIceCubes();
            collided = false;

            transform.localScale = originalSize;
            return;
        }
        //if ice cube lands outside the cup
        if (tableCollided)
        {
            //we start the timer.
            Debug.Log("TIMER STARTED");
            timer.Start();
            tableCollided = false;
			timerStarted = true;
            //TODO: Ice Cube Scaling Documentation Needed.
            transform.localScale = originalSize;
            return;
        }
	

    }
    /*
    This is a Unity Event Handler that activates when the user
    clicks anywhere in the screen.
    However, we need to do a conditional to make sure the clicked 
    object is the about-to-be-tossed.

    BUG TODO: What if we clicked on the ice cube while its tossed? 
              We must prevent that.
    */
    void OnMouseDown()
    {
        if (MainGameLoop.iceObj == gameObject)
            MainGameLoop.clickedOnCube = true;
    }
    /*
    This is a Unity Event Handler that activates when the user
    unclicks anywhere in the screen.
    
    Currently, this function is unused and serves as a placeholder.
    */
    void OnMouseUp()
    {

    }
    /*
    This is a Unity Event Handler that activates whenever a GameObject
    with Collider col enters the Collider of this script's gameobject (an ice cube).
    */
    void OnCollisionEnter(Collision col)
    {
        //if you collided within the cup.
        if (col.gameObject.name == "CupCollider")
        {
            collided = true;
            return;
        }
        //if you collided outside of the cup.
        //the extra conditional is because the waiting ice cubes
        //touch each other and the plate.
        if (gameObject == MainGameLoop.iceObj &&
            (col.gameObject.name == "Table"
            || col.gameObject.name == "Plate"
            || col.gameObject.name == "floor"
            || col.gameObject.name.Contains("Cube")))
        {
            tableCollided = true;
            return;
        }
    }
    /*
    This is a Unity Event Handler that activates when the timer it
    is attached to reaches the end of 3000 milliseconds (3 seconds)
    
   
    */
    void endTimer(object source, ElapsedEventArgs e)
    {
        iceIsOnTable = true;
        //set number of chances down by 1.
        missCount = missCount - 1;
        Debug.Log("Misses Left: " + missCount);
        //activate a game over if we fail
        if (missCount == 0)
        {
            MainGameLoop.gameEnds = true;
        }
        //stop the timer so this event handler isn't called every 3 seconds
        //onward.
        timer.Stop();
    }
    /*
    This function is usd to transition between ice cubes after one is successfully
    or unsuccessfully tossed.
    */
    void switchIceCubes()
    {
        //TODO: IceCubeScale documentation
        transform.localScale = originalSize;

        //This makes the tossed ice cube go behind the camera.
        Vector3 outOfView = new Vector3(MainGameLoop.outOfViewOri[MainGameLoop.currIceIndex, 0],
                                        MainGameLoop.outOfViewOri[MainGameLoop.currIceIndex, 1],
                                        MainGameLoop.outOfViewOri[MainGameLoop.currIceIndex, 2]);
        MainGameLoop.iceObj.transform.position = outOfView;
        //This sets the tossed ice cube to the default orientation.
        //TODO: Shouldn't this be (0,0,0,1)
        MainGameLoop.iceObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        //This negates gravity so the ice cube stays in one place.
        MainGameLoop.iceObj.GetComponent<Rigidbody>().isKinematic = true;
        MainGameLoop.iceObj.GetComponent<Rigidbody>().useGravity = false;
        //This negates prior movement so the ice cube doesn't float away (via Newton's 1st Law)
        MainGameLoop.iceObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

      
        //calls a helper to choose the next ice cube. If all ice cubes are used up, the
        //plate of ice cubes is reset.
        increaseIndex();

        //We change the focused ice cube (i.e. ice cube to toss) from the tossed
        //one to the next one on the plate with GameObject.Find.
        MainGameLoop.iceObj = GameObject.Find("Cube" + MainGameLoop.currIceIndex.ToString());
        //We set the new ice cube to the tossable position and orientation.
        Vector3 toss = new Vector3(MainGameLoop.tossOri[0],
                                      MainGameLoop.tossOri[1],
                                      MainGameLoop.tossOri[2]);
        MainGameLoop.iceObj.transform.position = toss;

        MainGameLoop.iceObj.transform.rotation = new Quaternion(MainGameLoop.tossOri[3],
                                                                MainGameLoop.tossOri[4],
                                                                MainGameLoop.tossOri[5],
                                                                MainGameLoop.tossOri[6]);
        //We still negate gravity and movement. They will activate only when our finger
        //motion tosses the ice cube.
        MainGameLoop.iceObj.GetComponent<Rigidbody>().isKinematic = true;
        MainGameLoop.iceObj.GetComponent<Rigidbody>().useGravity = false;
        MainGameLoop.iceObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        MainGameLoop.iceObj.AddComponent<IceCubeScale>();
        //Now, in MainGameLoop, we can toss the newly placed ice cube~!
        MainGameLoop.tossed = false;
        return;
    }
    
    /*
    This function increases the Main Game Loop's currIceIndex.
    
    */
    void increaseIndex()
    {
        //but if currIceIndex is 14, get a new plate of ice
        //and reset currIceIndex to 0.
        if (MainGameLoop.currIceIndex == 14)
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject currObj = GameObject.Find("Cube" + i.ToString());
                currObj.transform.position = new Vector3(MainGameLoop.plateOri[i, 0],
                                                         MainGameLoop.plateOri[i, 1],
                                                         MainGameLoop.plateOri[i, 2]);
                currObj.transform.rotation = new Quaternion(0, 0, 0, 1);
            }

            for (int i = 0; i < 15; i++)
            {
                GameObject currObj = GameObject.Find("Cube" + i.ToString());
                currObj.GetComponent<Rigidbody>().isKinematic = false;
                currObj.GetComponent<Rigidbody>().useGravity = true;
            }
            MainGameLoop.currIceIndex = 0;
        }
        else
        {
            MainGameLoop.currIceIndex++;
        }
    }
}

    