using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{
    TextMeshProUGUI resultScoreText;
    TextMeshProUGUI bestText;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        if (SceneManager.GetActiveScene().name == "FlappyBirdScene")
        {
            resultScoreText = transform.Find("ResultScoreText").GetComponent<TextMeshProUGUI>();
            bestText = transform.Find("BestText").GetComponent<TextMeshProUGUI>();
        }

    }

    public void SetScore(int currentScore, int bestScore)
    {
        resultScoreText.text = currentScore.ToString();
        bestText.text = bestScore.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
