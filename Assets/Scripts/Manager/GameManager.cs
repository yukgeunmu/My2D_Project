using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    private UIManager uiManager;
    public UIManager UIManager { get => uiManager; }
    public static GameManager Instance { get => gameManager; }

    public int cureentScore = 0;

    private int bestScore = 0;
    private const string BestScoreKey = "BestScore";

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;


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

        if (SceneManager.GetActiveScene().name == "TopDownScene" || SceneManager.GetActiveScene().name == "MainScene")
        {
            player = FindObjectOfType<PlayerController>();
            player.Init(this);
        }

        if (SceneManager.GetActiveScene().name == "TopDownScene")
        {
            enemyManager = GetComponentInChildren<EnemyManager>();
            enemyManager.Init(this);
        }


    }

    private void Start()
    {

    }


    public void UpdateScore()
    {
        if(SceneManager.GetActiveScene().name == "FlappyBirdScene")
        {
            if (bestScore < cureentScore)
            {
                bestScore = cureentScore;
            }

            PlayerPrefs.SetInt(BestScoreKey, bestScore);

            uiManager.gameOverUI.SetScore(cureentScore, bestScore);
        }

    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "FlappyBirdScene") uiManager.SetPlayGame();
        else if (SceneManager.GetActiveScene().name == "TopDownScene")
        {      
            uiManager.SetPlayGame();
            StartNextWave();
        }

    }

    void StartNextWave()
    {
       
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
    }



    public void AddScore(int score)
    {
        cureentScore += score;
        uiManager.gameUI.SetUI(cureentScore);
        
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새 씬에서 UIManager를 다시 찾아서 할당
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnDestroy()
    {
        // 구독 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



}
