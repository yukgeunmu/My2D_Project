using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : BaseUI
{
    TextMeshProUGUI scoreText;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        if(SceneManager.GetActiveScene().name == "FlappyBirdScene")
        {
            scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }

    }

    public void SetUI(int currentscore)
    {
        scoreText.text = currentscore.ToString();
    }


}
