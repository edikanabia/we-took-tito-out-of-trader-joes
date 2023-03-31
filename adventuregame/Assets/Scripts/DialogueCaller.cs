using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogueCaller : MonoBehaviour
{
    int currentSceneIndex;
    int sceneFrameCount = 0;
    string currentFilename;
    string theLastThingRoseSaid;
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
                        currentFilename = "oliver2";
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
                switch (tito.speakerName)
                {
                    case "Oliver":
                        currentFilename = "oliver4";
                        break;
                    case "Rose":
                        if (tito.npcSpeakingCount == 0)
                        {
                            currentFilename = "rose3";

                        }
                        else if (tito.npcSpeakingCount >= 6)
                        {
                            currentFilename = "rose5"; //rose explanation
                        }
                        else if (tito.npcSpeakingCount > 0 && tito.npcSpeakingCount < 6)
                        {
                            switch (gameManager.titoItems[0])
                            {
                                case "key club key":
                                    currentFilename = "key club key";

                                    break;
                                case "blue key":
                                    currentFilename = "blue key";
                                    break;
                                case "bent key":
                                    currentFilename = "bent key";
                                    break;
                                case "keyblade":
                                    currentFilename = "keyblade";
                                    break;
                                case "key west":
                                    currentFilename = "key west";
                                    break;
                                case "um key":
                                    currentFilename = "um key";
                                    break;
                                case "key lime pie":
                                    currentFilename = "key lime pie";
                                    break;

                            }

                        }
                        
                        break;
                    case "Jeanne":
                        currentFilename = "jeanne2";
                        break;
                }
            }
            else
            {
                //tito has no items
                if(gameManager.titoItems.Count == 0)
                {
                    switch (tito.speakerName)
                    {
                        case "Oliver":
                            currentFilename = "oliver3";
                            break;
                        case "Rose":
                            if(tito.npcSpeakingCount == 0)
                            {
                                currentFilename = "rose3";
                                
                            }
                            else
                            {
                                currentFilename = "rose4";
                                
                            }
                            break;
                        case "Jeanne":
                            currentFilename = "jeanne1";
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
                                currentFilename = "rose5"; //rose explanation
                            }

                            else
                            {
                                //rose describes the first item in your inventory
                                switch (gameManager.titoItems[0])
                                {
                                    case "key club key":
                                        currentFilename = "key club key";
                                        
                                        break;
                                    case "blue key":
                                        currentFilename = "blue key";
                                        break;
                                    case "bent key":
                                        currentFilename = "bent key";
                                        break;
                                    case "keyblade":
                                        currentFilename = "keyblade";
                                        break;
                                    case "key west":
                                        currentFilename = "key west";
                                        break;
                                    case "um key":
                                        currentFilename = "um key";
                                        break;
                                    case "key lime pie":
                                        currentFilename = "key lime pie";
                                        break;

                                }
                                
                            }
                            break;
                        case "Jeanne":
                            currentFilename = "jeanne1";
                            break;

                        case "Oliver":
                            currentFilename = "oliver3";
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
                switch (tito.speakerName)
                {
                    case "Jeanne":
                        currentFilename = "ghost trick";
                        break;
                    case "Traitor Joe":
                        currentFilename = "game win";
                        break;
                }
            }
            else
            {
                if(gameManager.titoItems.Count == 0)
                {
                    switch (tito.speakerName)
                    {
                        case "Jeanne":
                            currentFilename = "jeanne3";
                            break;
                        case "Traitor Joe":
                            currentFilename = "traitorjoe0";
                            break;
                    }
                }
                else
                {
                    switch (tito.speakerName)
                    {
                        case "Jeanne":
                            currentFilename = gameManager.titoItems[0];
                            break;
                        case "Traitor Joe":
                            currentFilename = "game lose";
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
            case "oliver2": //any of the scene ending oliver or joe scripts
                return true;
            case "oliver4":
                return true;
            case "game win":
                return true;
            case "game lose":
                return true;
            default:
                return false;
        }
    }

}
