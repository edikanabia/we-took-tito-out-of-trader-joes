using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerbreadManScript : MonoBehaviour
{
    Rigidbody2D gingerBody;
    public float speed = 0.5f;
    Vector2 movement = new Vector2(0, 1f);

    // Start is called before the first frame update
    void Start()
    {
        gingerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gingerBody.MovePosition(gingerBody.position + movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        speed = -speed;
    }
}
