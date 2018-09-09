using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WallJump : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [SerializeField]
    private float HorizontalForce;

    [SerializeField]
    private float JumpForce;

    [SerializeField]
    private CircleCollider2D castingCollider;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];

    //Keep track of the player's ability to Wall Jump
    private bool WallJumpLeftReady = false;
    private bool WallJumpRightReady = false;
    private bool JumpPossible = false;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    //OnCollisionEnter code carbon copy of Stay code.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //make sure a regular jump ins't possible
        int casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);


        int i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
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


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.4)
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


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.4)
            {
                WallJumpLeftReady = true;
                
            }
            i++;
        }
    }

    //Has to be colliding for the flags allowing the jump to be true.
    private void OnCollisionStay2D(Collision2D collision)
    {
        //make sure a regular jump ins't possible
        int casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);


        int i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
            {
                JumpPossible = true;
            }
            i++;
        }

        casthits = 0;

        //Right Wall Jump check
        casthits = castingCollider.Cast(new Vector2(1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.4)
            {
                WallJumpRightReady = true;
                
            }
            i++;
        }

        //Left Wall Jump Check
        casthits = castingCollider.Cast(new Vector2(-1, 0), raycastHit2s, 1);

        i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.4)
            {
                WallJumpLeftReady = true;
                
            }
            i++;
        }
    }

    //make sure to set the flags equal to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        //reset the bools so you can't abuse it.
        if (collision.gameObject.tag == "Ground")
        {
            WallJumpLeftReady = false;
            WallJumpRightReady = false;
            JumpPossible = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
       
        //The wall jump itself.
        if (Input.GetKeyDown(KeyCode.Space) && WallJumpLeftReady && !JumpPossible)
        {
            //reset the velocity so the wall jump has the intended effect
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(HorizontalForce, JumpForce), ForceMode2D.Impulse);
             
            
            
            WallJumpLeftReady = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && WallJumpRightReady && !JumpPossible)
        {
            //reset the velocity so the wall jump has the intended effect
            rb.velocity = new Vector2(0, 0);
           
            rb.AddForce(new Vector2(-HorizontalForce, JumpForce), ForceMode2D.Impulse);
            
            
            WallJumpRightReady = false;
        } 
    }
}
