using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerScript : MonoBehaviour
{
    public List<string> speakerLines;
    public int lineNumber = 0;

    public bool firstTimeSpeaking = true;

    public Sprite portrait;

    GameManager gameManager;
    PlayerController tito;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (firstTimeSpeaking)
        {
            firstTimeSpeaking = false;
            lineNumber = 1;
        }
    }
}
