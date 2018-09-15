using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : Implant
{
    private List<System.Type> incompatibleTypes = new List<System.Type>() { typeof(Jump) };

    [SerializeField]
    private CircleCollider2D castingCollider;

    private Rigidbody2D rb;
    [SerializeField]
    private float JumpForce;
    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];
    private bool JumpReady = false;
    private bool colliding = false;

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
        try
        {
            castingCollider = GetComponents<CircleCollider2D>()[1];
        }
        catch
        {
            
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale == 0) { return; }

        int casthits = 0;
        try
        {
            casthits = castingCollider.Cast(new Vector2(0, -1), raycastHit2s, 1);
        }
        catch
        {

        }

        int i = 0;

        while ( i < casthits )
        {
            rayHit = raycastHit2s[i];
            
            
            if (collision.gameObject.tag == "Ground" && System.Math.Abs(rayHit.point.y - rayHit.centroid.y) < 0.25)
            {
                JumpReady = true;
                Debug.Log("Jump Reset by Enter2D");
                colliding = true;
            }
            i++;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        bool wjright = gameObject.GetComponent<WallJump>().WallJumpRightReady;
        bool wjleft = gameObject.GetComponent<WallJump>().WallJumpLeftReady;

        if (collision.gameObject.tag == "Ground" && !wjright && !wjleft )
        {
            JumpReady = true;    
            Debug.Log("Jump Reset by Stay2D");
            colliding = true;
        }
        else if(collision.gameObject.tag == "Ground")
        {
            colliding = true;
        }
       
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       if( collision.gameObject.tag == "Ground" )
       {
            JumpReady = false;
            Debug.Log("JumpReady false because of Exit2D");
       }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && JumpReady) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            JumpReady = false;
        }

        float distance = 1.0f;
        castingCollider.Cast(new Vector2(0, -1), raycastHit2s, distance);
       
     
    
       rayHit = raycastHit2s[0];
       if( System.Math.Abs(rayHit.point.y - rayHit.centroid.y) < 0.1f && rayHit.collider != null )
       {
           JumpReady = true;
             Debug.Log("Jump Reset by Update");
       
       }
       else
       {
            if(!colliding)
            {
                //updates everyframe, so if we walk off we need to set it to false
                JumpReady = false;
                Debug.Log("Jump false by Update");
            }
          
       }
    }
    private void OnDrawGizmos()
    {
        foreach( RaycastHit2D ray in raycastHit2s)
        {
            Gizmos.DrawRay(ray.centroid, ray.point - ray.centroid);
        }
        
    }
}
