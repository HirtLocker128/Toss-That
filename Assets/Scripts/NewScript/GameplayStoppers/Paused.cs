using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paused : MonoBehaviour {

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
        GameObject.Find("Table").GetComponent<Melter>().pauseGame();
        GameObject.Find("Table").GetComponent<CupFall>().pauseGame();
        if (currIceCube.GetComponent<Tosser>().enabled)
        {
            currIceCube.GetComponent<Tosser>().pauseGame();
        }
        else if (currIceCube.GetComponent<IceCollider>().enabled)
        {
            currIceCube.GetComponent<IceCollider>().pauseGame();
        }
        else if (currIceCube.GetComponent<NextIceCube>().enabled)
        {
            currIceCube.GetComponent<NextIceCube>().pauseGame();
        }

    }

    void ContinueGame()
	{
        GameObject.Find("Table").GetComponent<Melter>().unpauseGame();
        GameObject.Find("Table").GetComponent<CupFall>().unpauseGame();
        if (currIceCube.GetComponent<Tosser>().enabled)
        {
            currIceCube.GetComponent<Tosser>().unpauseGame();
        }
        else if (currIceCube.GetComponent<IceCollider>().enabled)
        {
            currIceCube.GetComponent<IceCollider>().unpauseGame();
        }
        else if (currIceCube.GetComponent<NextIceCube>().enabled)
        {
            currIceCube.GetComponent<NextIceCube>().unpauseGame();
        }
    }

    public void updateIceCube(GameObject nextOne)
    {
        currIceCube = nextOne;
    }
}
