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
        Debug.Log("게임이 시작되었습니다.");
    }

    public void Restart()
    {
        Debug.Log("왜 안될가");
        GameManager.Instance.UIManager.SetPlayGame();
    }

}
