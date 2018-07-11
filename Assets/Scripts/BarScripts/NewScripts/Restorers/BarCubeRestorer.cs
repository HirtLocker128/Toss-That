using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCubeRestorer : MonoBehaviour {
    /*
    Private Static Variables that store original values
    */
    //Used to restore the size of an ice cube after the toss.
    private static Vector3 originalSize;

    //Used to restore positions after a toss.
    private static Vector3 tossPos;
    private static Quaternion tossOri;
    private static Vector3[] platePos;
    private static Vector3[] outOfViewPos;
    // Use this for initialization
    void Awake () {
        //I chose to save Cube1's size in case lag makes Melter go first and 
        //melt the first ice cube (named Cube0).
        GameObject cube = GameObject.Find("Cube1");
        originalSize = cube.transform.localScale;

        outOfViewPos = new Vector3[15];
        for (int i = 0; i < 15; i++)
        {
            outOfViewPos[i] = new Vector3(-7 + i, -3, -5);
        }
        platePos = new Vector3[15];
        for (int i = 0; i < 15; i++)
        {
            GameObject iObj = GameObject.Find("Cube" + i.ToString());
            platePos[i] = iObj.transform.position;
        }

        GameObject firstObj = GameObject.Find("Cube0");
        tossPos = firstObj.transform.position;
        tossOri = firstObj.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getOutOfView(int index)
    {
        return outOfViewPos[index];
    }
    public Vector3 getPlatePos(int index)
    {
        return platePos[index];
    }
    public Vector3 getTossPos()
    {
        return tossPos;
    }

    public Quaternion getTossOri()
    {
        return tossOri;
    }

    public Vector3 getSize()
    {
        return originalSize;
    }
}
