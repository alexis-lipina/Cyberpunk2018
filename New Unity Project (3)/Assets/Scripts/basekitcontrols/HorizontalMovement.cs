using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : Implant
{
    private List<System.Type> incompatibleTypes = new List<System.Type>();

    [SerializeField] private GameObject UI;

    private Rigidbody2D rb;
    [SerializeField]
    private float WalkForce;

    public int LeftRight = 1; //1 = right, -1 = left

    private bool HasWallJumped = false; 
    [SerializeField]
    private CircleCollider2D castingCollider;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];

    //These keep track of the player's ability to walljump.
    private bool WallJumpLeftReady = false;
    private bool WallJumpRightReady = false;
    private bool JumpPossible = false;

    public override List<Type> IncompatibleTypes
    {
        get
        {
            return incompatibleTypes;
        }
    }

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
        HasWallJumped = false;

        //make sure a regular jump ins't possible
        int casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);


        int i = 0;

        while (i < casthits)
        {
            rayHit = raycastHit2s[i];


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
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


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
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


            if (collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
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
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                UI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                UI.SetActive(true);
            }
        }
        if(Time.timeScale == 0) { return; }






        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.Space) && WallJumpRightReady && !JumpPossible)
            {
                HasWallJumped = true;
            }
            
            //If the player has wall jumped we don't want the player to just scale up the wall they 
            if(HasWallJumped)
            {

            }
            else
            { 
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(WalkForce, 0), ForceMode2D.Impulse);
                LeftRight = 1;
               
            }
            
        }
        else
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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
                LeftRight = -1;
            }
            
        }
        else
        {
            rb.AddForce(new Vector2(-rb.velocity.x, 0));
        }












        gameObject.GetComponent<PlayerAnimator>().MoveVector /*+*/= rb.velocity; //animate the player movement
    }
}
