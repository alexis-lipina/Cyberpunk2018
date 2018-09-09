using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WallHang : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float HangForce;



    
    [SerializeField]
    private CircleCollider2D castingCollider;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];

    //These keep track of the player's ability to walljump.
    private bool WallJumpLeftReady = false;
    private bool WallJumpRightReady = false;
    private bool JumpPossible = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    //These methods are used to track the WallJumpStatus of the Player, so we don't apply the force when we want to wall jump.
    //They are functionally the same as the ones found in WallJump.cs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Reset HasHappened
        

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


            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
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
        
        if(  Input.GetKey(KeyCode.RightArrow) && WallJumpRightReady && !Input.GetKey(KeyCode.Space) )
        {
            rb.sharedMaterial.friction = HangForce;
            if( System.Math.Abs(rb.velocity.y) > 0.5 )
            {
                rb.AddForce(new Vector2(5, -rb.velocity.y), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
            }
            
            

        }
        else
        if( Input.GetKey(KeyCode.LeftArrow)  && WallJumpLeftReady && !Input.GetKey(KeyCode.Space) )
        {

            rb.sharedMaterial.friction = HangForce;

            if (System.Math.Abs(rb.velocity.y) > 0.5)
            {
                rb.AddForce(new Vector2(-5, -rb.velocity.y), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
            }

           
        }
        else
        {
            rb.sharedMaterial.friction = 0;
        }

       
    }
}
