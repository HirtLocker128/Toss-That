using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBar : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    void OnMouseDown()
    {
        SceneManager.LoadSceneAsync("BarScene");
    }
    // Update is called once per frame
    void Update()
    {

    }
}