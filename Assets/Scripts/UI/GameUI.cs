using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameUI : BaseUI
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI waveText;
    Slider hpSlider;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "TopDownScene")
            UpdateHPSlider(1);
    }

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
        else if(SceneManager.GetActiveScene().name == "TopDownScene")
        {
            waveText = transform.Find("Wave/WaveText").GetComponent<TextMeshProUGUI>();
            hpSlider = transform.Find("HPBar/Slider").GetComponent<Slider>();
        }

    }

    public void SetUI(int currentscore)
    {
        scoreText.text = currentscore.ToString();
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }




}
