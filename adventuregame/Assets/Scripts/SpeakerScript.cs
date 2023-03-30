using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour
{
    public List<string> speakerLines;
    public int lineNumber = 0;

    public bool firstTimeSpeaking = true;

    public Sprite speakerPortrait;
    public string speakerName;
    public int speakerState = 0;

    GameManager gameManager;
    PlayerController tito;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //scenario 0 - beginning of the game
        //scenario 1 no items
        //scenario 2 wrong items
        //scenario 3 right items


    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (firstTimeSpeaking)
        {
            firstTimeSpeaking = false;
            speakerState++;
        }
    }
}
