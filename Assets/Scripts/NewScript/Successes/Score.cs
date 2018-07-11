using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {
    private static int score = 0;

    public static void scored()
    {
        score = score + 1;
        Debug.Log("Scored");
    }

    public static int getScore()
    {
        return score;
    }
}
