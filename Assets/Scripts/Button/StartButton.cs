using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public void FlapBirdStart()
    {
        SceneManager.LoadScene("FlappyBirdScene");
    }

    public void StartGameButton()
    {
        PlayerFlappy player = FindObjectOfType<PlayerFlappy>();
        GameManager.Instance.StartGame();
        player.isTime = true;
    }



}
