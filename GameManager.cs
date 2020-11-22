using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{

    public GameState CurrentGameState = GameState.Menu;
    public static GameManager sharedInstance; // Unique instance to control the game
    private PlayerController controller;
    public int CollectedObject = 0;


    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && CurrentGameState != GameState.InGame){
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            MenuManager.SharedInstance.ShowMainMenu();
            MenuManager.SharedInstance.HideGameOverMenu();
            MenuManager.SharedInstance.HideInGameMenu();
            // TODO: menu logic
        }
        else if (newGameState == GameState.InGame)
        {
            // TODO: game logic
            LevelManager.SharedInstance.RemoveAll();
            LevelManager.SharedInstance.GenerateInitialLevelBlocks();
            controller.StartGame();
            MenuManager.SharedInstance.ShowInGameMenu();
            MenuManager.SharedInstance.HideMainMenu();
            MenuManager.SharedInstance.HideGameOverMenu();
        }
        else // Game Over
        {
            MenuManager.SharedInstance.ShowGameOverMenu();
            MenuManager.SharedInstance.HideMainMenu();
            MenuManager.SharedInstance.HideInGameMenu();
            CollectedObject = 0;
            // TODO: Game Over scene
        }

        this.CurrentGameState = newGameState;

    }

    public void CollectObject(Collectable collectable)
    {
        CollectedObject += collectable.Value;
    }

    
}
