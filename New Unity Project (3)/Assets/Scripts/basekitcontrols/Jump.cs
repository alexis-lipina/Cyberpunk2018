using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{


    [SerializeField]
    private CircleCollider2D castingCollider;

    private Rigidbody2D rb;
    [SerializeField]
    private float JumpForce;
    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];
    private bool JumpReady = false;
    
    
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
            
            
            if (collision.gameObject.name == "Outline" && rayHit.fraction < 0.25)
            {
                JumpReady = true;
                
            }
            i++;
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
        if (Input.GetKeyDown(KeyCode.Space) && JumpReady) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            JumpReady = false;
        }

        try
        {
            castingCollider.Cast(new Vector2(0, -1), raycastHit2s);
        }
        catch
        {
            
        }
        
        rayHit = raycastHit2s[0];
        if( rayHit.fraction < 0.25 )
        {
            JumpReady = true;
            
        }
        else
        {
            //updates everyframe, so if we walk off we need to set it to false
            JumpReady = false; 
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
