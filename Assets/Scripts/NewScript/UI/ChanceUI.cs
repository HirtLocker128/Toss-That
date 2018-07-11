using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceUI : MonoBehaviour {
    // Use this for initialization
    private int chanceVal;
    private Text chance;
    void Awake()
    {
        chance = GetComponent<Text>();
        chanceVal = 0;
        ChancesLeft.resetChances();
    }

    // Update is called once per frame
    void Update () {
        chance.text = "Chances: " + ChancesLeft.getChancesLeft().ToString();
    }
}
