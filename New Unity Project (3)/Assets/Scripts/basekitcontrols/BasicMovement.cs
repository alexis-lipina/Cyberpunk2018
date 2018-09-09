using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float WalkForce;

    private bool JumpReady = false;
    private bool WallJumpLeftReady = false;
    private bool WallJumpRightReady = false;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.name == "Outline")
        {
            JumpReady = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       if( collision.gameObject.name == "Outline" )
       {
            JumpReady = false;
       }
    }
    // Update is called once per frame
    void Update ()
    {
        if(  Input.GetKey(KeyCode.RightArrow) )
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(WalkForce, 0), ForceMode2D.Impulse);
        }
        else
        if( Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(-WalkForce, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-rb.velocity.x, 0));
        }

        

        if (Input.GetKeyDown(KeyCode.Space) && JumpReady) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            JumpReady = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && WallJumpLeftReady)
        {

        }
        else if (Input.GetKeyDown(KeyCode.Space) && WallJumpRightReady)
        {

        }
    }
}
