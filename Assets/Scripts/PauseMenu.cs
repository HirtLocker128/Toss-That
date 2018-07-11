using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
	}

    public void PauseButton ()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    } 

    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("mainMenu");
    }

    public void MuteMusic ()
    {
        if (!AudioListener.pause)
        {
            Debug.Log("Mute Music");
            AudioListener.pause = true;

        }else if (AudioListener.pause)
        {
            Debug.Log("Unmute Music");
            AudioListener.pause = false;
        }
    }
}
