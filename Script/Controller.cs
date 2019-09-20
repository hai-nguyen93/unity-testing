using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    Rigidbody2D rigi;
    public float speed = 10f;   
    Vector2 velocity;
    bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        isGround = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forceX = Input.GetAxis("Horizontal") * speed;
        float forceY = 0f;
        if (isGround)
        {
            if (Input.GetButton("Jump"))
            {
                forceY = 500f;
                isGround = false;
            }
        }
       
        velocity = new Vector2(forceX, forceY);
        
        rigi.AddForce(velocity);
    }
    //Ham check xem Player co cham dat khong
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

}
