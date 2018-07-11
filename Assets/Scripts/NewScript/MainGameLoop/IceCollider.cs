//Imports for Unity C# Programming
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Import for Timer class.
using System.Timers;

/*
This class is used for Ice Collision Behavior
All ice cubes are linked to this script.

However, this script is ONLY enabled when the ice cube
is in tossed mode. Otherwise, the script is disabled.

This Script's Behavior:
Checks where the ice landed.
*/
public class IceCollider : MonoBehaviour {
    /*
    Private Substate Variables
    */
    private enum state { landedInCup, landedAnywhereElse, timerRunning, delayOver, none, pause, paused, unpause };
    private state substate;
    private state savedState;
    /*
    Private Members
    */
    //For the 3 second delay if a cup fails to land in the cup.
    private Timer delayTimer = null;
    private Vector3 savedVelocity;
    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function only activates when the Scene is loaded, even if the script is disabled.
    If the scene is reloaded within itself, we need to make our own function: RepeatStart.

    This function initializes all member variables.
    */
    void Awake () {
        substate = IceCollider.state.none;
        delayTimer = new Timer();
        delayTimer.Elapsed += new ElapsedEventHandler(eventTimer);
        delayTimer.Interval = 3000;
        delayTimer.Stop();
        //needed to prevent OnEnable from activating when the script is attached to the
        //next ice obj. This will make OnDisable activate unfortunately.
        gameObject.GetComponent<IceCollider>().enabled = false;
    }
    
    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function activates every 16.67 milliseconds (for 60 frames-per-second).

    Here, the Update function checks what this script's substate is, then takes the
    appopriate action. Once this script's work is done, it transfers control to NextIceCube.
    */
    void Update () {
		if (substate == IceCollider.state.landedInCup)
        {
            //When cube islands in cup play IceInCup
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Sounds/IceInCup") as AudioClip;
            audioSource.Play();
            //enable NextIceCube
            gameObject.GetComponent<NextIceCube>().enabled = true;
            gameObject.GetComponent<NextIceCube>().successThrow();
            //disable this script, wil set substate to none in OnDisable
            //no logic should come after this code block and after the if-elseif's here.
            gameObject.GetComponent<IceCollider>().enabled = false;

        }
        else if (substate == IceCollider.state.landedAnywhereElse)
        {
            //When cube is lands somewhere else play throw sound
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Sounds/IceMiss2") as AudioClip;
            audioSource.Play();

            delayTimer.Start();
            substate = IceCollider.state.timerRunning;
        }
        else if (substate == IceCollider.state.timerRunning)
        {

        }
        else if (substate == IceCollider.state.delayOver)
        {
            //enable NextIceCube
            gameObject.GetComponent<NextIceCube>().enabled = true;
            gameObject.GetComponent<NextIceCube>().failThrow();
            //disable this script, wil set substate to none in OnDisable
            //no logic should come after this code block and after the if-elseif's here.
            gameObject.GetComponent<IceCollider>().enabled = false;

        }
        else if (substate == IceCollider.state.none)
        {
            
        }
        else if (substate == IceCollider.state.pause)
        {
            if (substate == IceCollider.state.timerRunning)
            {
                delayTimer.Enabled = false;
            }
            savedVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            
            substate = IceCollider.state.paused;
        }
        else if (substate == IceCollider.state.paused)
        {

        }
        else if (substate == IceCollider.state.unpause)
        {
            substate = savedState;
            if (savedState == IceCollider.state.timerRunning)
            {
                delayTimer.Enabled = true;
            }
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().velocity = savedVelocity;
        }
    }

    /*
    This is a Unity Event Handler defined by MonoBehaviour.
    It activates whenever this script is enabled, in the moment
    the ice cube is tossed.
    */
    void onEnable()
    {
        substate = IceCollider.state.none;
        delayTimer.Stop();
    }
    /*
    This is a Unity Event Handler defined by MonoBehaviour.
    It activates whenever this script is disabled, when the ice cube
    has landed.
    */
    void OnDisable()
    {
        substate = IceCollider.state.none;
        delayTimer.Stop();
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
            substate = IceCollider.state.landedInCup;
            return;
        }
        //if you collided outside of the cup.
        //the extra conditional is because the waiting ice cubes
        //touch each other and the plate.
        if (col.gameObject.name == "Table"
            || col.gameObject.name == "Plate"
            || col.gameObject.name == "floor"
            || col.gameObject.name.Contains("Cube"))
        {
            substate = IceCollider.state.landedAnywhereElse;
            return;
        }
        //if ice cube collided with person
        if (col.gameObject.name == "DJ Collider")
        {
            //When cube lands on person play reaction
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load("Sounds/GlassCupDrop") as AudioClip;
            audioSource.Play();
            return;
        }

    }

    /*
   This is a Unity Event Handler that activates when the timer it
   is attached to reaches the end of 3000 milliseconds (3 seconds)
   */
    void eventTimer(object source, ElapsedEventArgs e)
    {
        //stop the timer so this event handler isn't called every 3 seconds
        //onward.
        delayTimer.Stop();
        substate = IceCollider.state.delayOver;
    }

    public void pauseGame()
    {
        savedState = substate;
        substate = IceCollider.state.pause;
    }
    public void unpauseGame()
    {
        substate = IceCollider.state.unpause;
    }
}
