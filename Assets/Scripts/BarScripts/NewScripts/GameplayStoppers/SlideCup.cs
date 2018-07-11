using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCup : MonoBehaviour {
	/*
	public float speed;
	public GameObject pointA;
	public GameObject pointB;
	public bool reverseMove = false;
	private float startTime;
	private float journeyLength; 
   */
	public Transform[] target;
	public Rigidbody cupRb;
	GameObject cup;
	//public float speed;

	public static System.Random rnd = new System.Random();
	private int speed = rnd.Next(5,10);

	private int current;
   


	// Use this for initialization
	void Start () {
		cup = GameObject.Find ("CupRigidbody");
		//startTime = Time.time;
		//journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);


	}

	// Update is called once per frame
	void Update () {

		if (transform.position == target [1].position) 
		{
			cupRb.isKinematic = false;
			Destroy (gameObject, 3f);
		}


		if (transform.position != target [current].position)
		{		
			Vector3 pos = Vector3.MoveTowards (transform.position, target [current].position, speed * Time.deltaTime);
			GetComponent<Rigidbody> ().MovePosition (pos);
		} 
		else current = (current + 1) % target.Length;
			
		}
		
}
/*
		//float distCovered = (Time.time - startTime) * speed;
//float fracJourney = distCovered / journeyLength;


if (reverseMove)
{
	transform.position = Vector3.Lerp(pointB.transform.position, pointA.transform.position, fracJourney);
	//transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);


}
else
{
	transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);
}
if ((Vector3.Distance(transform.position, pointB.transform.position) == 0.0f || Vector3.Distance(transform.position, pointA.transform.position) == 0.0f)) //Checks if the object has travelled to one of the points
{
	if (reverseMove)
	{
		reverseMove = false;
	}
	else
	{
		reverseMove = true;
	}
	startTime = Time.time;
}
*/
