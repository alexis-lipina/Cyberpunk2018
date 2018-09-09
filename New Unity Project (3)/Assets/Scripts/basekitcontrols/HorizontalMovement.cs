using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float WalkForce;

    public bool LeftRight = true; //True = right, false = left

    private bool HasWallJumped = false; 
    [SerializeField]
    private CircleCollider2D castingCollider;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];

    //Keep these for now (add to WallJump)
    private bool WallJumpLeftReady = false;
    private bool WallJumpRightReady = false;
    private bool JumpPossible = false;
    //Keep these for now (add to WallJump)
    //private bool WallJumpLeftReady = false;
    //private bool WallJumpRightReady = false;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //make sure a regular jump ins't possible
        int casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);


        int i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
            {
                JumpPossible = true;
                HasWallJumped = false;
            }
            i++;
        }

        casthits = 0;

        //Right
        casthits = castingCollider.Cast(new Vector2(1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
            {
                WallJumpRightReady = true;
                HasWallJumped = false;
            }
            i++;
        }

        //Left
        casthits = castingCollider.Cast(new Vector2(-1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
            {
                WallJumpLeftReady = true;
                
            }
            i++;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //make sure a regular jump ins't possible
        int casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);


        int i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
            {
                JumpPossible = true;
            }
            i++;
        }

        casthits = 0;

        //Right
        casthits = castingCollider.Cast(new Vector2(1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.4)
            {
                WallJumpRightReady = true;

            }
            i++;
        }

        //Left
        casthits = castingCollider.Cast(new Vector2(-1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.4)
            {
                WallJumpLeftReady = true;

            }
            i++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //reset the bools so you can't abuse it.
        if (collision.gameObject.name == "Outline")
        {
            WallJumpLeftReady = false;
            WallJumpRightReady = false;
            JumpPossible = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(  Input.GetKey(KeyCode.RightArrow) )
        {
            if (Input.GetKeyDown(KeyCode.Space) && WallJumpRightReady && !JumpPossible)
            {
                HasWallJumped = true;
            }
            
            if(HasWallJumped)
            {

            }
            else
            { 
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(WalkForce, 0), ForceMode2D.Impulse);
                LeftRight = true;
            }
            
        }
        else
        if( Input.GetKey(KeyCode.LeftArrow) )
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && WallJumpLeftReady && !JumpPossible)
            {
                HasWallJumped = true;
            }

            if (HasWallJumped)
            {
               
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-WalkForce, 0), ForceMode2D.Impulse);
                LeftRight = false;
            }
            
        }
        else
        {
            rb.AddForce(new Vector2(-rb.velocity.x, 0));
        }

        

       /* //For the wall jump behavior.
        if(Input.GetKeyDown(KeyCode.Space) && WallJumpLeftReady)
        {
       
        }
        else if (Input.GetKeyDown(KeyCode.Space) && WallJumpRightReady)
        {
       
        } */
    }
}
