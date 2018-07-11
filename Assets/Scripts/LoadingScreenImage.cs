using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenImage : MonoBehaviour {


    public Sprite loadingScreenPic1;
    public Sprite loadingScreenPic2;
    public Sprite loadingScreenPic3;
    public Sprite loadingScreenPic4;

    int randomPicture;

    public Image loadingScreen;
	// Use this for initialization
	void Start () {

        loadingScreen = gameObject.GetComponentInChildren<Image>();
        
        randomPicture = Random.Range(1, 4);
        switch (randomPicture)
        {
            case 1:
                loadingScreen.sprite = loadingScreenPic1;
                break;

            case 2:
                loadingScreen.sprite = loadingScreenPic2;
                break;

            case 3:
                loadingScreen.sprite = loadingScreenPic3;
                break;

            case 4:
                loadingScreen.sprite = loadingScreenPic4;
                break;

        }


    }
	
}
