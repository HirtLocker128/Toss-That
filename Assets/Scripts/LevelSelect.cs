using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    public GameObject levelSelect;
    public GameObject levelButtons;
    public GameObject modeSelect;
    public GameObject modeButtons;
    public GameObject mainCamera;
    public GameObject mainMenu;
    public GameObject levelCamera;
    public GameObject modeCamera;
    public GameObject tutorialCamera;
    public GameObject tutorial;
    public GameObject tutorialButtons;

    //public GameModeToggle boolInfo;

	// Use this for initialization
	void Start () {


	}

    public void ShowGameMode()
    {
        modeSelect.SetActive(true);
        modeButtons.SetActive(true);
        levelSelect.SetActive(false);
        levelButtons.SetActive(false);
        mainCamera.SetActive(false);
        modeCamera.SetActive(true);
        levelCamera.SetActive(false);
        tutorial.SetActive(false);
        tutorialButtons.SetActive(false);
        tutorialCamera.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        modeSelect.SetActive(false);
        modeButtons.SetActive(false);
        levelSelect.SetActive(false);
        levelButtons.SetActive(false);
        mainCamera.SetActive(true);
        modeCamera.SetActive(false);
        levelCamera.SetActive(false);
        tutorial.SetActive(false);
        tutorialButtons.SetActive(false);
        tutorialCamera.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowTutorial()
    {
        modeSelect.SetActive(false);
        modeButtons.SetActive(false);
        levelSelect.SetActive(false);
        levelButtons.SetActive(false);
        mainCamera.SetActive(false);
        modeCamera.SetActive(false);
        levelCamera.SetActive(false);
        tutorial.SetActive(true);
        tutorialButtons.SetActive(true);
        tutorialCamera.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowLevels()
    {
        levelSelect.SetActive(true);
        levelButtons.SetActive(true);
        modeSelect.SetActive(false);
        modeButtons.SetActive(false);
        mainCamera.SetActive(false);
        modeCamera.SetActive(false);
        levelCamera.SetActive(true);
        tutorial.SetActive(false);
        tutorialButtons.SetActive(false);
        tutorialCamera.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void HideLevels()
    {
        levelSelect.SetActive(false);
        levelButtons.SetActive(false);
        modeSelect.SetActive(false);
        modeButtons.SetActive(false);
        mainCamera.SetActive(true);
        modeCamera.SetActive(false);
        levelCamera.SetActive(false);
        tutorial.SetActive(false);
        tutorialButtons.SetActive(false);
        tutorialCamera.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetTimed()
    {
        //GameObject g = GameObject.FindGameObjectWithTag(BoolKeeper);
        // boolInfo = g.GetComponent<GameModeToggle>();
        //boolInfo.gameMode = true;
        GameObject.Find("GameModeToggle").GetComponent<GameModeToggle>().gameMode = true;
    }

    public void SetUntimed()
    {
        //GameObject g = GameObject.FindGameObjectWithTag(BoolKeeper);
        GameObject.Find("GameModeToggle").GetComponent<GameModeToggle>().gameMode = false;
        //boolInfo.gameMode = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
