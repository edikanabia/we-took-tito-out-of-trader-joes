using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Globalization;

public class PlayerController : MonoBehaviour
{

    ////Tito's inventory (a series of booleans)
    //public List<string> titoItems;
    //int maxInventory = 4;
    //int groceries = 0;
    //int treats = 0;
    //public int itemsCount = 0;

    public GameManager gameManager;
    public DialogueCaller caller;
    int nextSceneIndex;


    //can he finish the game?!?!?
    public bool isReadyToCheckout = false;
    public string speakerName;
    public int npcSpeakingCount;

    //movement variables
    public float playerSpeed;
    public Rigidbody2D playerRB;
    private Vector2 _movement;
    private Vector2 _previousPosition;

    public Animator playerAnimator;


    //public TMP_Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
        _previousPosition = playerRB.position;
        caller = GameObject.Find("DialogueCaller").GetComponent<DialogueCaller>();
        //dialogue.enabled = false;
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameManager.gameEnd)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;

            if (gameManager.speaking)
            {
                _movement.x = 0;
                _movement.y = 0;
                playerRB.MovePosition(transform.position);
            }
            else
            {
                _movement.x = Input.GetAxisRaw("Horizontal");
                _movement.y = Input.GetAxisRaw("Vertical");
            }

            if (gameManager.groceries >= gameManager.groceryThreshold)
            {
                isReadyToCheckout = true;
            }
            else
            {
                isReadyToCheckout = false;
            }


            



        }

    }

    //updates at time intervals 
    //(rather than by frames, 
    //so as to not tie things to framerate).
    private void FixedUpdate()
    {
        if (!gameManager.gameEnd)
        {
            // this handles all movement without using inputs/if statements. thanks merry!
            playerRB.MovePosition(playerRB.position + _movement * playerSpeed);

            //animation
            if (playerRB.position == _previousPosition) //isn't moving
            {
                playerAnimator.SetBool("moving", false);
            }
            else
            {
                playerAnimator.SetBool("moving", true);

                if (_movement.x < 0) //left
                {
                    playerAnimator.SetInteger("direction", 1);
                }

                if (_movement.x > 0) //right
                {
                    playerAnimator.SetInteger("direction", 2);
                }

                if (_movement.y > 0) //down
                {
                    playerAnimator.SetInteger("direction", 0);

                }

                if (_movement.y < 0) //up
                {
                    playerAnimator.SetInteger("direction", 3);
                }
            }



        }

        else
        {
            playerAnimator.SetBool("moving", false);
        }

        _previousPosition = playerRB.position;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Grocery") || collision.CompareTag("Treat"))
        {
            //checks to see if Tito has enough space in his inventory
            if (gameManager.titoItems.Count < gameManager.maxInventory)
            {
                if (collision.CompareTag("Grocery"))
                {
                    gameManager.groceries++;
                }
                else if (collision.CompareTag("Treat"))
                {
                    gameManager.treats++;
                }

                gameManager.titoItems.Add(collision.name); //adds the name of the item to inventory

                //adding the image to the UI
                SpriteRenderer currentSprite = collision.gameObject.GetComponent<SpriteRenderer>(); //gets sprite from collected item


                gameManager.slotImages[gameManager.itemsCount].sprite = currentSprite.sprite; //sets image sprite to currentsprite
                gameManager.ListItem(); //enables icon

                gameManager.itemsCount++;

                collision.gameObject.SetActive(false); //deactivates gameobject
                                                       //Destroy(collision.gameObject); //makes sure you can't collect it twice, destroys object

            }
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Speaker"))
        {
            speakerName = collision.gameObject.name;
            gameManager.DialogueBox(caller.UpdateDialogue());
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if speaking is true + you're supposed to switch once the text is done,
        //do not switch until after speaking  is false
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Speaker")
        {
            npcSpeakingCount = collision.gameObject.GetComponent<SpeakerScript>().timesSpokenTo;
            gameManager.dialogueBoxObj.SetActive(false);
        }
        
    }


}
