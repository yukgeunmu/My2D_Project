using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get => gameManager; }

    private int cureentScore = 0;

    private void Awake()
    {
        gameManager = this;
    }


    public void GameOver()
    {
        Debug.Log("Game OVer");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("FlappyBirdScene");
    }

    public void AddScore(int score)
    {
        cureentScore += score;
    }


}
