using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResturantSpawnpoints : MonoBehaviour {

		public Transform[] SpawnPoints1;
		//public Transform[] SpawnPoints2;
		//public Transform[] SpawnPoints3;

		public float spawnTime1 = 5.5f;
		//public float spawnTime2 = 5.5f;
		//public float spawnTime3 = 5.5f;

        private GameObject MyGlassware;


		//public GameObject Glassware;
		public GameObject[] Glassware;


		// Use this for initialization
		void Start () {
        //InvokeRepeating ("SpawnCups1", 0f, spawnTime1);
            SpawnCups1();
            MyGlassware = GameObject.FindGameObjectWithTag("Glassware");

        //InvokeRepeating ("SpawnCups2", 0.3f, spawnTime2);
        //InvokeRepeating ("SpawnCups3", 0.6f, spawnTime3);


    }

    // Update is called once per frame
    void Update () {

        StartCoroutine(MyCoroutine());

    }


    IEnumerator MyCoroutine()
    {
        //This is a coroutine
        RestartSpawning();

        yield return 0;    //Wait one frame        
    }

    void RestartSpawning()
    {
        if (MyGlassware == null)
        {
            Destroy(MyGlassware);
            SpawnCups1();
            MyGlassware = GameObject.FindGameObjectWithTag("Glassware");
        }
    }
    void SpawnCups1()
		{
			int spawnIndex = Random.Range (0, SpawnPoints1.Length); // Randomly selected number from spawnPoint array

			int objectIndex = Random.Range (0, Glassware.Length);


			Instantiate(Glassware[objectIndex], SpawnPoints1[spawnIndex].position, SpawnPoints1[spawnIndex].rotation);
		}

	/*void SpawnCups2()
	{
		int spawnIndex = Random.Range (0, SpawnPoints2.Length); // Randomly selected number from spawnPoint array

		int objectIndex = Random.Range (0, Glassware.Length);


		Instantiate(Glassware[objectIndex], SpawnPoints2[spawnIndex].position, SpawnPoints2[spawnIndex].rotation);
	}

	void SpawnCups3()
	{
		int spawnIndex = Random.Range (0, SpawnPoints3.Length); // Randomly selected number from spawnPoint array

		int objectIndex = Random.Range (0, Glassware.Length);


		Instantiate(Glassware[objectIndex], SpawnPoints3[spawnIndex].position, SpawnPoints3[spawnIndex].rotation);
	}*/

	}