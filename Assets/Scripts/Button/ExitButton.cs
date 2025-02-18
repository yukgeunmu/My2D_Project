using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject panel;

    public void CrossePanel()
    {
        panel.SetActive(false);
    }

}
