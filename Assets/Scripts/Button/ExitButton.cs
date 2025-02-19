using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public GameObject panel;


    public void CrossePanel()
    {
        panel.SetActive(false);
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
        UIManager.isFirst = true;
        Time.timeScale = 1;
    }

}
