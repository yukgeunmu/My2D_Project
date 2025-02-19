using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : BaseUI
{
    GameObject angelPanel;
    GameObject demonPanel;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            angelPanel = transform.Find("AngelPanel").gameObject;
            demonPanel = transform.Find("DemonPanel").gameObject;
        }

    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public void SetActivePanelMainScene(NPCName name, int inOut)
    {
        if(inOut == 0)
        {
            angelPanel.SetActive(NPCName.Angel == name);
            demonPanel.SetActive(NPCName.DeMon == name);
        }
        else if(inOut == 1)
        {
            angelPanel.SetActive(false);
            demonPanel.SetActive(false);
        }
    }


}
