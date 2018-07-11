using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melter : MonoBehaviour {
    private GameObject currIceCube;
    private int temperature;
    private float sizeReduction = 0.001f;
    private float reductionIncrease = 0.001f;
    private Vector3 sizeForGameOver = new Vector3(0.1f, 0.1f, 0.1f);
    private enum state { melting, none, pause, paused, unpause };
    private state substate;
    private state savedState;
    // Use this for initialization
    void Awake () {
        currIceCube = GameObject.Find("Cube0");
        substate = Melter.state.melting;
        temperature = 70;
	}
	
	// Update is called once per frame
	void Update () {
		if (substate == Melter.state.melting && !PauseMenu.GameIsPaused)
        {
            currIceCube.transform.localScale = currIceCube.transform.localScale - sizeReduction * currIceCube.transform.localScale;
            if (currIceCube.transform.localScale.x < sizeForGameOver.x)
            {
                GameObject.Find("gameOverCanvas").GetComponent<GameOver>().GameOverSet();
                substate = Melter.state.none;
            }
        }
        else if (substate == Melter.state.none)
        {

        }
        if (substate == Melter.state.pause)
        {
            substate = Melter.state.paused;
        }
        if (substate == Melter.state.paused)
        {

        }
        if (substate == Melter.state.unpause)
        {
            substate = savedState;
        }
    }

    public void updateIceCube(GameObject nextOne)
    {
        substate = Melter.state.none;
        currIceCube.transform.localScale = GameObject.Find("Table").GetComponent<CubeRestorer>().getSize();
        currIceCube = nextOne;
        substate = Melter.state.melting;
    }

    public void pauseGame()
    {
        savedState = substate;
        substate = Melter.state.pause;
    }
    public void unpauseGame()
    {
        substate = Melter.state.unpause;
    }

    public void stopMelter()
    {
        substate = Melter.state.none;
    }

    public int getTemperature()
    {
        return temperature;
    }

    public void temperatureIncreases()
    {
        temperature += 5;
        sizeReduction += reductionIncrease;
    }
}
