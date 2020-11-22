using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager SharedInstance;
    public Canvas MenuCanvas;
    public Canvas GameOverCanvas;
    public Canvas InGameCanvas;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }

        InGameCanvas.enabled = false;
        GameOverCanvas.enabled = false;
    }

    public void ShowMainMenu()
    {
        MenuCanvas.enabled = true;
        
    }

    public void ShowGameOverMenu()
    {
            GameOverCanvas.enabled = true;
            GameOverCanvas.GetComponentInChildren<AudioSource>().Play();
    }

    public void ShowInGameMenu()
    {
        InGameCanvas.enabled = true;
        InGameCanvas.GetComponentInChildren<AudioSource>().Play();
    }

    public void HideMainMenu()
    {
        MenuCanvas.enabled = false;
        MenuCanvas.GetComponentInChildren<AudioSource>().Stop();
    }

    public void HideGameOverMenu()
    {
        GameOverCanvas.enabled = false;
        GameOverCanvas.GetComponentInChildren<AudioSource>().Stop();
    }

    public void HideInGameMenu()
    {
        InGameCanvas.enabled = false;
        InGameCanvas.GetComponentInChildren<AudioSource>().Stop();

    }

    public void ExitGame()
    {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #else
         Application.Quit(); // whit all Systems
      #endif
    }

    
}
