using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    public GameObject LevelOne;
    public GameObject LevelTwo;
    public GameObject LevelThree;
    public GameObject LevelFour;

    public Text progressText;


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        LevelOne.SetActive(false);
        LevelTwo.SetActive(false);
        LevelThree.SetActive(false);
        LevelFour.SetActive(false);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            //This code shows current percentage. Commented out due to look.
            //progressText.text = progress * 100f + "%";
            //Debug.Log(progress);

            yield return null;
        }
    }

}
