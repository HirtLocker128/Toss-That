using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This class is used to give a transparent feel to the ice cubes.
*/
public class TransparencyInitiator : MonoBehaviour {

    /*
    This is a UnityEngine function defined by MonoBehaviour.
    This function sets the script's object (an ice cube) to have a transparent look.
    */
    void Start () {
        Renderer rend = GetComponent<Renderer>();
        //Color's parameters are Red, Green, Blue, Alpha (Opaque-ness)
        rend.material.SetColor("_Color", new Color(0.0f, 1.0f,1.0f, 0.5f));
        rend.material.SetColor("_DiffColor", new Color(0.0f, 1.0f, 1.0f, 0.5f));
        rend.material.SetColor("_SpecColor", new Color(0.0f, 1.0f, 1.0f, 0.5f));
    }
}
