using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = gameManager;
        }
        else
        {
            Destroy(gameManager.gameObject);
        }
    }



}
