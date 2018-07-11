using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScript : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        //this script will be on the GameObject that has the animator directly.
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ice Cube")
        {
            anim.SetBool("Dodge", true);
        }
        else
        {
            anim.SetBool("Dodge", false);
        }

    }

}