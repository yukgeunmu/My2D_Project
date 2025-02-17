using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdSceneLoad : MonoBehaviour
{
    public void LoadFlappyBirdScen()
    {
        SceneManager.LoadScene("FlappyBirdScene");
    }

}
