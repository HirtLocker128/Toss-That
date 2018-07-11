using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocations : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 5.5f;

	//public GameObject Glassware;
	public GameObject[] Glassware;

	public Animator grabAnimation;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnCups", 1f, spawnTime);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnCups()
	{
		int spawnIndex = Random.Range (0, SpawnPoints.Length); // Randomly selected number from spawnPoint array

		int objectIndex = Random.Range (0, Glassware.Length);

		grabAnimation.Play ("Place_Cup", -1, 0f);

		Instantiate(Glassware[objectIndex], SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation);
	}
		
}
