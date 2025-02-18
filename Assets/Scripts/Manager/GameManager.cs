using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int cureentScore = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
