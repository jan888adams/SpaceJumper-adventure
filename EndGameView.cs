using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndGameView : MonoBehaviour
{
    public Text MaxScoreText;
    public Text CoinText;
    private PlayerController Controller;
    // Start is called before the first frame update
    void Start()
    {
       Controller = GameObject.Find("player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.CurrentGameState == GameState.GameOver)
        {
            int maxCoins = PlayerPrefs.GetInt("CoinText", 0);
            float maxScore = PlayerPrefs.GetFloat("MaxScoreText", 0);

            CoinText.text = maxCoins.ToString();
            MaxScoreText.text = maxScore.ToString("f1");


        }
    }
}
