using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChancesLeft {

    	
    private static int chancesLeft = 3;

//	public Camera camera1;
//	public Camera gameeOverCamera;

    public static void decrementChance()
    {
        chancesLeft = chancesLeft - 1;
        if (chancesLeft == 0)
        {
            GameObject.Find("gameOverCanvas").GetComponent<GameOver>().GameOverSet();
        }
    }

	public static void resetChances()
	{
		chancesLeft = 3;
	}

    public static void infiniteChances()
    {
        chancesLeft = 1000;
    }

    public static int getChancesLeft()
    {
        return chancesLeft;
    }
}
