using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMelter : MonoBehaviour {
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
        substate = BarMelter.state.melting;
        temperature = 70;
	}
	
	// Update is called once per frame
	void Update () {
		if (substate == BarMelter.state.melting && !PauseMenu.GameIsPaused)
        {
            currIceCube.transform.localScale = currIceCube.transform.localScale - sizeReduction * currIceCube.transform.localScale;
            if (currIceCube.transform.localScale.x < sizeForGameOver.x)
            {
                GameObject.Find("gameOverCanvas").GetComponent<GameOver>().GameOverSet();
                substate = BarMelter.state.none;
            }
        }
        else if (substate == BarMelter.state.none)
        {

        }
        if (substate == BarMelter.state.pause)
        {
            substate = BarMelter.state.paused;
        }
        if (substate == BarMelter.state.paused)
        {

        }
        if (substate == BarMelter.state.unpause)
        {
            substate = savedState;
        }
    }

    public void updateIceCube(GameObject nextOne)
    {
        substate = BarMelter.state.none;
        currIceCube.transform.localScale = GameObject.Find("Table").GetComponent<BarCubeRestorer>().getSize();
        currIceCube = nextOne;
        substate = BarMelter.state.melting;
    }

    public void pauseGame()
    {
        savedState = substate;
        substate = BarMelter.state.pause;
    }
    public void unpauseGame()
    {
        substate = BarMelter.state.unpause;
    }

    public void stopMelter()
    {
        substate = BarMelter.state.none;
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
