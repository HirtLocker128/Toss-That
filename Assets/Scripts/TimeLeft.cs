using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    public int timeLeft = 60;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
        ChancesLeft.infiniteChances();

    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("Time Left : " + timeLeft);

        if (timeLeft < 0)
        {
            StopCoroutine("LoseTime");

            countdownText.text = "Times Up!";

            GameObject.Find("gameOverCanvas").GetComponent<GameOver>().GameOverSet();

        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}