//Imports for Unity C# Programming
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Imports for Scene Transition
using UnityEngine.SceneManagement;

/*
This class is used for handing the 
states of the Gameplay. This class
is attached to the Canvas Object.

This Script's Behavior:
1. Initialize and reset game states.
2. Use states to check if an ice cube is to be tossed, is already tossed, game is paused,
   game is reset, etc.
3. Tossing an ice cube.

IMPROVEMENTS TODO: 
1. Replace all states with 1 enumerator since only one substate is true while the rest are false.
2. Separate the ice cube tossing mechanic into a separate class to improve the class's readability.
3. The Scripts are really disorganized. We should research new ways to organize our scripts. Via
   other game companies, Unity forums, etc.
*/

public class MainGameLoop : MonoBehaviour {
    /*
    Public Static Object Restorers - used for re-initializing 
    after a scene reset.
    */
    public static float[] tossOri = { 0, 0, 0, 0, 0, 0, 0 };
    public static float[,] plateOri = { { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 },
                                        { 0,0,0 }};
    public static float[,] outOfViewOri = { { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 },
                                            { 0,0,0 }};
    /*
    Public Static UI Members
    */
    public static int score;
    public static int temperature;
    /*
    Public Static Game States
    */
    public static bool tossed;
    public static bool gameStart;
    public static bool gameEnds;
    public static bool gameOver;
    public static bool pause;
    /*
   Public Static UI Variables
   */
    private GameObject scoreObj;
    private GameObject tempObj;

    /*
   Public Static Cameras
   */
    public static GameObject camera1;
    public static GameObject camera2;

    //swipe mechanic (with mouse clicks)
    //slowerX, slowerY, and expanderZ decrease and increase swiping power.
    /*
    Variables used for Swipe Mechanic

    TODO: Separate the swiping into its own class.
    */
    private Vector3 touchPosition;
    private float slowerX = 0.025F;
    private float slowerY = 0.025F;
    private float expanderZ = 1F;
    /*
    Public Static Members used to interact with IceCubeScript.

    TODO: Separate the swiping into its own class.
    */
    public static bool clickedOnCube;
    public static int currIceIndex;
    public static GameObject iceObj;

    /*
    TODO: Document UI
    */
    public bool paused = false;
    /*
    TODO: Document IceCubeScale work
    */
    private IceCubeScale IceScale;
    private Vector3 originalSize;



   /*
   This is a UnityEngine function defined by MonoBehaviour.
   This function only activates when the Scene is loaded.
   If the scene is reloaded, we need to make our own function: RepeatStart.

   This function initializes all member variables.

   TODO: Add UI and IceCubeScale Documentation
   */
    void Start () {
        // TODO: Documentation for declaring the pausePanel object

        //This code is repeated in RepeatStart
        score = 0;
        temperature = 0;
        tossed = false;
        gameStart = true;
        gameEnds = false;
        gameOver = false;
        pause = false;
        currIceIndex = 0;
        clickedOnCube = false;
        //Initialize UI Items
        scoreObj = GameObject.Find("Score");
        tempObj = GameObject.Find("Temperature");
        touchPosition = new Vector3(0, 0, 0);
        //Initialize the first tossable ice cube.
        iceObj = GameObject.Find("Cube0");
        tossOri[0] = iceObj.transform.position.x;
        tossOri[1] = iceObj.transform.position.y;
        tossOri[2] = iceObj.transform.position.z;
        tossOri[3] = iceObj.transform.rotation.w;
        tossOri[4] = iceObj.transform.rotation.x;
        tossOri[5] = iceObj.transform.rotation.y;
        tossOri[6] = iceObj.transform.rotation.z;
        //TODO: Chan's work
        iceObj.AddComponent<IceCubeScale>();

        //TODO: "storage to hold original size". Where is it?


        //Save the positions of the ice cubes on the plate. This is used
        //whenever we want to refill the plate or reload this scene.
        GameObject currObj = null;
        for (int i = 0; i < 15; i++)
        {
            currObj = GameObject.Find("Cube" + i.ToString());
            plateOri[i, 0] = currObj.transform.position.x;
            plateOri[i, 1] = currObj.transform.position.y;
            plateOri[i, 2] = currObj.transform.position.z;

            outOfViewOri[i, 0] = -7 + i;
            outOfViewOri[i, 1] = -3;
            outOfViewOri[i, 2] = -10;
        }
        //Initialize the cameras which point to the game, and game over screens.
        camera1 = GameObject.Find("Main Camera");
        camera2 = GameObject.Find("gameOverCamera");
        camera2.SetActive(false);

        
        originalSize = iceObj.transform.localScale;

    }

    /*
    This function NEEDS TO BE ACTIVATED whenever the scene is reloaded (i.e. pause + reset
    game or gameover + restart)
    The states and other variables are reset to their original values.
    */
    public void RepeatStart()
    {
        //This code is repeated in Start
        score = 0;
        temperature = 0;
        tossed = false;
        gameStart = true;
        gameEnds = false;
        gameOver = false;
        pause = false;
        currIceIndex = 0;
        clickedOnCube = false;
        //Reinitialize UI Items
        scoreObj = GameObject.Find("Score");
        tempObj = GameObject.Find("Temperature");
        touchPosition = new Vector3(0, 0, 0);
        
        //Restore Cube Positions
        iceObj = GameObject.Find("Cube0");

        iceObj.transform.position = new Vector3(tossOri[0],tossOri[1],tossOri[2]);
        iceObj.transform.rotation = new Quaternion(MainGameLoop.tossOri[3],
                                                                MainGameLoop.tossOri[4],
                                                                MainGameLoop.tossOri[5],
                                                                MainGameLoop.tossOri[6]);

        MainGameLoop.iceObj.GetComponent<Rigidbody>().isKinematic = true;
        MainGameLoop.iceObj.GetComponent<Rigidbody>().useGravity = false;
        MainGameLoop.iceObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //Restore camera and enable options.
        camera1 = GameObject.Find("Main Camera");
        camera2 = GameObject.Find("gameOverCamera");
        camera2.SetActive(false);

        // TODO: documentation for recovering the original size for all.
        // disabling other cube's from melting.
        GameObject currObj = null;
        for (int i = 0; i < 15; i++)
        {
            currObj = GameObject.Find("Cube" + i.ToString());
            currObj.transform.localScale = originalSize;
            currObj.GetComponent<IceCubeScale>().enabled = false;

        }
        currObj = GameObject.Find("Cube" + 0);
        IceCubeScale a = currObj.GetComponent<IceCubeScale>();
        a.enabled = true;
    }

    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function activates every 16.67 milliseconds (for 60 frames-per-second).

    Here, the Update function does an action if a certain state variable is set
    to true. Main Game State Changes should be restricted to here.

    TODO: Clean up code from this point on. I can barely understand the changes.
    */
    void Update ()
    {
        //TODO: documentation for "check current instance and whether it is paused"
        GameObject currentObj = iceObj;
        IceCubeScript refforBool = currentObj.GetComponent<IceCubeScript>();
        paused = refforBool.gamePaused;

        if (gameEnds)
        {
            gameEnds = false;
            cameraChange();
            gameOver = true;
        }
        else if (gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                iceObj.transform.localScale = originalSize;
                //done so when the reload occurs, GameObject.Find finds the cameras.
                camera2.SetActive(true);
                camera1.SetActive(true);
                //Reload everything
                for (int i = 1; i < 15; i++) //start at 1 because 0 is iceCube
                {
                    GameObject.Find("Cube" + i.ToString()).GetComponent<IceCubeScript>().RepeatStart(i);
                }
                GameObject.Find("Table").GetComponent<CupFallScript>().RepeatStart();

                // when gameover called, currently hold ice cube restored
                
                this.RepeatStart(); //sets gameOver to false implicitly.
            }
        }
        else if (paused)
        {
            // return the size to the current
            iceObj.transform.localScale = originalSize;
        }
        else if (gameStart)
        {
            gameStart = false;
        }
        else if (tossed == false)
        {
            /**
              * Source: https://www.youtube.com/watch?v=VbtwwsOb4YI
              */
            //Mouse is clicked on current ice cube.
            if (Input.GetMouseButtonDown(0) && clickedOnCube)
            {
                touchPosition = Input.mousePosition;
                Debug.Log(touchPosition);
            }
            //After the mouse is clicked on the current ice cube,
            //the mouse is released.
            if (Input.GetMouseButtonUp(0) && clickedOnCube)
            {
                //get the vector representing the difference between
                //the position of the mouse release and the position of the
                //mouse click.
                Vector2 deltaSwipe = Input.mousePosition - touchPosition;
                Debug.Log("curr pos:" + Input.mousePosition);
                Debug.Log("orig pos:" + touchPosition);
                Debug.Log("delta: " + deltaSwipe);

                //calculate the z velocity with the pythagorean theorm.
                float zVal = 0;
                if (deltaSwipe.y < 0)
                {
                    zVal = -1 * expanderZ * Mathf.Sqrt(Mathf.Pow(deltaSwipe.x * slowerX, 2) + Mathf.Pow(deltaSwipe.y * slowerY, 2));
                }
                else
                {
                    zVal = expanderZ * Mathf.Sqrt(Mathf.Pow(deltaSwipe.x * slowerX, 2) + Mathf.Pow(deltaSwipe.y * slowerY, 2));
                }
                Debug.Log("zVal: " + zVal);
                //set the iceObj's velocity for the ice cube's click and release.
                iceObj.GetComponent<Rigidbody>().velocity = new Vector3(deltaSwipe.x * slowerX,
                    deltaSwipe.y * slowerY, zVal);
                //allow the iceCube to move.
                MainGameLoop.iceObj.GetComponent<Rigidbody>().isKinematic = false;
                MainGameLoop.iceObj.GetComponent<Rigidbody>().useGravity = true;
                //allow the cup to fall
                //GameObject.Find("Cup").GetComponent<Rigidbody>().isKinematic = false;
                //GameObject.Find("Cup").GetComponent<Rigidbody>().useGravity = true;
                //update game states
                tossed = true;
                clickedOnCube = false;

                iceObj.GetComponent<IceCubeScale>().enabled = false;

            }
        }
	}
    


    void cameraChange()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
		if (IceCubeScript.timerStarted == true) {
			IceCubeScript.timer.Stop ();
		}

        
        // disable the scale function
        IceScale = iceObj.GetComponent<IceCubeScale>();
        IceScale.enabled = false;

        // disble iceObj script as well

       //CubeScript = iceObj.GetComponent<IceCubeScript>();

        //CubeScript.enabled = false;

    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
		if (IceCubeScript.timerStarted == true) {
			IceCubeScript.timer.Start ();
		}

        IceScale = iceObj.GetComponent<IceCubeScale>();
        IceScale.enabled = true;

        //enable the scripts again

        //CubeScript = iceObj.GetComponent<IceCubeScript>();

        //CubeScript.enabled = true;
    }


}
