using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{
    TextMeshProUGUI resultScoreText;
    TextMeshProUGUI bestText;
    TextMeshProUGUI currentWave;
    TextMeshProUGUI bestWave;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (SceneManager.GetActiveScene().name == "FlappyBirdScene")
        {
            resultScoreText = transform.Find("ResultScoreText").GetComponent<TextMeshProUGUI>();
            bestText = transform.Find("BestText").GetComponent<TextMeshProUGUI>();
        }
        if(SceneManager.GetActiveScene().name == "TopDownScene")
        {
            currentWave = transform.Find("WaveResult/CurrentScore").GetComponent<TextMeshProUGUI>();
            bestWave = transform.Find("WaveResult/BestScore").GetComponent<TextMeshProUGUI>();
        }

    }

    public void SetScore(int currentScore, int bestScore)
    {
        resultScoreText.text = currentScore.ToString();
        bestText.text = bestScore.ToString();
    }

    public void SetWaveScore(int wave, int _bestWave)
    {
        currentWave.text = wave.ToString();
        bestWave.text = _bestWave.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
