using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarPaused : MonoBehaviour {

	private bool paused;
	private Text pauseText;
    private GameObject currIceCube;
	// Use this for initialization
	void Start () {
		paused = false;
		pauseText = GetComponent<Text> ();
		pauseText.text = "Pause";
        currIceCube = GameObject.Find("Cube0");
	}
	void OnMouseDown(){
		if (paused == true) {
			ContinueGame ();
			pauseText.text = "Pause";
			paused = false;
		}
		else if (paused == false) {
			PauseGame ();
			pauseText.text = "Resume";
			paused = true;
		}
	}
	// Update is called once per frame
	void Update () {

	}
    void PauseGame()
    {
        GameObject.Find("Table").GetComponent<BarMelter>().pauseGame();
        GameObject.Find("Table").GetComponent<CupFall>().pauseGame();
        if (currIceCube.GetComponent<BarTosser>().enabled)
        {
            currIceCube.GetComponent<BarTosser>().pauseGame();
        }
        else if (currIceCube.GetComponent<BarIceCollider>().enabled)
        {
            currIceCube.GetComponent<BarIceCollider>().pauseGame();
        }
        else if (currIceCube.GetComponent<BarNextIceCube>().enabled)
        {
            currIceCube.GetComponent<BarNextIceCube>().pauseGame();
        }

    }

    void ContinueGame()
	{
        GameObject.Find("Table").GetComponent<BarMelter>().unpauseGame();
        GameObject.Find("Table").GetComponent<CupFall>().unpauseGame();
        if (currIceCube.GetComponent<BarTosser>().enabled)
        {
            currIceCube.GetComponent<BarTosser>().unpauseGame();
        }
        else if (currIceCube.GetComponent<BarIceCollider>().enabled)
        {
            currIceCube.GetComponent<BarIceCollider>().unpauseGame();
        }
        else if (currIceCube.GetComponent<BarNextIceCube>().enabled)
        {
            currIceCube.GetComponent<BarNextIceCube>().unpauseGame();
        }
    }

    public void updateIceCube(GameObject nextOne)
    {
        currIceCube = nextOne;
    }
}
