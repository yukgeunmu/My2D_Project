using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    GameOver

}


public class UIManager : MonoBehaviour
{
    public HomeUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    private UIState currentState;

    public GameObject gameOverPanel;

    private static bool isFirst = true;

    private void Awake()
    {

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        if(SceneManager.GetActiveScene().name == "FlappyBirdScene")
        {
            if (isFirst)
            {
                ChangeState(UIState.Home);
                isFirst = false;
            }
            else
            {
                ChangeState(UIState.Game);
                PlayerFlappy player = FindObjectOfType<PlayerFlappy>();
                player.isTime = true;
            }
        }
        else if(SceneManager.GetActiveScene().name == "MainScene")
        {
            ChangeState(UIState.Home);
        }

    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game); 
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);

    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }   





}
