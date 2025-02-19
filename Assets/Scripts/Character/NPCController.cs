using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCName
{
    Angel,
    DeMon
}

public class NPCController : MonoBehaviour
{
    public UIManager uiManager;
    public NPCName npcName;


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch(npcName)
            {
                case NPCName.Angel:
                    uiManager.homeUI.SetActivePanelMainScene(npcName, 0);
                    break;
                case NPCName.DeMon:
                    uiManager.homeUI.SetActivePanelMainScene(npcName, 0);
                    break;

            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            uiManager.homeUI.SetActivePanelMainScene(npcName, 1);
        }    
    }
}
