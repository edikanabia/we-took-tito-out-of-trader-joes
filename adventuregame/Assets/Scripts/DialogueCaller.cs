using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogueCaller : MonoBehaviour
{
    int currentSceneIndex;
    int sceneFrameCount = 0;
    string currentFilename;
    GameManager gameManager;
    PlayerController tito;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tito = GameObject.Find("Player").GetComponent<PlayerController>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        currentFilename = "opening" + currentSceneIndex;
        Debug.Log(currentFilename);
        sceneFrameCount = 0;
    }

    private void Update()
    {
        sceneFrameCount += 1;
        if (sceneFrameCount == 1)
        {
            gameManager.DialogueBox(currentFilename);
        }
        
    }

    public void ChangeScene(int index)
    {        
        SceneManager.LoadScene(index);
        currentSceneIndex = index;
        sceneFrameCount = 0;
    }

    public string UpdateDialogue()
    {
        if(currentSceneIndex == 1) //scene1 dialogue
        {
            if (tito.isReadyToCheckout) //tito has all items scenarios
            {
                switch (tito.speakerName)
                {
                    case "Oliver":
                        currentFilename = "oliver3";
                        break;
                    case "Rose":
                        currentFilename = "rose2";
                        break;
                    case "Terry":
                        currentFilename = "terry3";
                        break;
                    case "Jeanne":
                        currentFilename = "jeanne0";
                        break;
                }
            }
            else
            {
                //tito has no items scenarios
                if (gameManager.titoItems.Count == 0)
                {
                    switch (tito.speakerName)
                    {
                        case "Oliver":
                            currentFilename = "oliver0";
                            break;
                        case "Rose":
                            currentFilename = "rose0";
                            break;
                        case "Terry":
                            if (tito.npcSpeakingCount == 0)
                            {
                                currentFilename = "terry0";
                            }
                            else
                            {
                                currentFilename = "terry1";
                            }
                            break;
                        case "Jeanne":
                            currentFilename = "jeanne0";
                            break;
                    }
                }

                //tito doesn't have the right items scenario
                else if (gameManager.titoItems.Count > 0 && gameManager.groceries < gameManager.groceryThreshold)
                {
                    switch (tito.speakerName)
                    {
                        case "Oliver":
                            currentFilename = "oliver1";
                            break;
                        case "Rose":
                            currentFilename = "rose1";
                            break;
                        case "Terry":
                            currentFilename = "terry2";
                            break;
                        case "Jeanne":
                            currentFilename = "jeanne0";
                            break;
                    }
                }
            }
        }
        
        else if(currentSceneIndex == 2) //scene2 dialogue
        {
            //tito has the right items scenarios
            if (tito.isReadyToCheckout)
            {

            }
            else
            {
                //tito has no items
                if(gameManager.titoItems.Count == 0)
                {
                    switch (tito.speakerName)
                    {
                        case "Oliver":
                            currentFilename = "oliver0";
                            break;
                        case "Rose":
                            break;
                        case "Jeanne":
                            break;
                    }
                }
                //tito has items but not the right ones
                else if(gameManager.titoItems.Count > 0 && gameManager.groceries < gameManager.groceryThreshold)
                {
                    switch (tito.speakerName)
                    {
                        case "Rose":
                            if (tito.npcSpeakingCount >= 6)
                            {
                                currentFilename = ""; //rose explanation
                            }
                            else
                            {
                                //rose comments on every object
                                switch (gameManager.titoItems[0])
                                {
                                    case "key lime pie":
                                        break;
                                }
                            }
                            break;
                        case "Jeanne":
                            break;
                        case "Oliver":
                            break;
                    }
                    
                }
            }

        }

        else if (currentSceneIndex == 3) //scene3 dialogue
        {
            //tito has the right items scenarios
            if (tito.isReadyToCheckout)
            {

            }
            else
            {
                //tito has no items
                if (gameManager.titoItems.Count == 0)
                {

                }
                //tito has items but not the right ones
                else if (gameManager.titoItems.Count > 0 && gameManager.groceries < gameManager.groceryThreshold)
                {
                    if (tito.speakerName == "Rose")
                    {
                        if (tito.npcSpeakingCount >= 6)
                        {
                            currentFilename = ""; //rose explanation
                        }
                    }
                    //rose comments on every object
                    switch (gameManager.titoItems[0])
                    {
                        case "key lime pie":
                            currentFilename = "";
                            break;
                            
                    }
                }
            }
        }

        return currentFilename;
    }

    public bool SwitchNow()
    {
        switch (UpdateDialogue())
        {
            case "oliver3": //any of the scene ending oliver or joe scripts
                return true;
            default:
                return false;
        }
    }

}
