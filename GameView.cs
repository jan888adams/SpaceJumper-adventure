using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameView : MonoBehaviour
{
    public Text ScoreText;
    public Text CurrentCoinText;

    private PlayerController Controller;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GameObject.Find("player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.CurrentGameState == GameState.InGame )
        {
            int coins = GameManager.sharedInstance.CollectedObject;
            float score = Controller.GetTravelledDistance();
            //float maxScore = 0f;

            CurrentCoinText.text = coins.ToString();
            ScoreText.text = score.ToString("f1");

        }
        
    }
}
