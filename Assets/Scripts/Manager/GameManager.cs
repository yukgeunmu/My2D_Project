using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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
    private const string BestWaveKey = "BestWave";

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;
    private int bestWave = 0;



    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        if (gameManager == null)
        {
            gameManager = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if(gameManager != null)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        bestWave = PlayerPrefs.GetInt(BestWaveKey, 0);

        if (SceneManager.GetActiveScene().name == "TopDownScene" || SceneManager.GetActiveScene().name == "MainScene")
        {
            
            player = FindObjectOfType<PlayerController>();
            player.Init(this);

            _playerResourceController = player.GetComponent<ResourceController>();
            _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP); // 제거
            _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP); // 다시 추가
        }

        //if (SceneManager.GetActiveScene().name == "TopDownScene")
        //{
        //    enemyManager = GetComponentInChildren<EnemyManager>();
        //    enemyManager.Init(this);
        //}


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

        if(SceneManager.GetActiveScene().name == "TopDownScene")
        {
            if(bestWave < currentWaveIndex)
            {
                bestWave = currentWaveIndex;
            }

            PlayerPrefs.SetInt(BestWaveKey, bestWave);

            uiManager.gameOverUI.SetWaveScore(currentWaveIndex, bestWave);

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
        uiManager.ChangeWave(currentWaveIndex);

        EnemyManager.instance.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        EnemyManager.instance.StopWave();
        uiManager.SetGameOver();
        gameManager.UpdateScore();
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
