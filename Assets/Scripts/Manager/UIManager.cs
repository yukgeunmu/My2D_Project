using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;

public enum UIState
{
    Home,
    Game,
    GameOver

}


public class UIManager : MonoBehaviour
{
    public HoemUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    private UIState currentState;

    public GameObject gameOverPanel;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HoemUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(UIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);    
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
        return;
    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }   





}
