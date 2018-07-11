using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {
    private enum state { none, gameover };
    private state substate;
    // Use this for initialization
    void Awake () {
        substate = GameOver.state.none;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GameOverSet()
    {
		GameObject cam = GameObject.Find ("Main Camera");
		GameObject gameOverCam = GameObject.Find ("gameOverCamera");
		cam.SetActive(false);
		gameOverCam.SetActive(true);
        /*Destroy(GameObject.Find("Table"));
        for (int i = 0; i < 15; i++ )
            Destroy(GameObject.Find("Table" + i.ToString()));*/
        substate = GameOver.state.gameover;
    }
    void OnMouseDown()
    {
        SceneManager.LoadSceneAsync("mainMenu");
    }
}
