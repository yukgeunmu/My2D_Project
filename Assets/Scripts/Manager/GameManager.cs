using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    private UIManager uiManager;
    public UIManager UIManager { get => uiManager; }
    public static GameManager Instance { get => gameManager; }

    public bool isFirst = false;

    public int cureentScore = 0;

    private int bestScore = 0;
    private const string BestScoreKey = "BestScore";


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameManager);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if(gameManager != null)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    private void Start()
    {
      
    }

    public void UpdateScore()
    {
        if(bestScore < cureentScore)
        {
            bestScore = cureentScore;
        }

        PlayerPrefs.SetInt(BestScoreKey, bestScore);

        uiManager.gameOverUI.SetScore(cureentScore, bestScore);
    }



    public void StartGame()
    {
        uiManager.SetPlayGame();
    }


    public void AddScore(int score)
    {
        cureentScore += score;
        uiManager.gameUI.SetUI(cureentScore);
        
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ������ UIManager�� �ٽ� ã�Ƽ� �Ҵ�
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnDestroy()
    {
        // ���� ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



}
