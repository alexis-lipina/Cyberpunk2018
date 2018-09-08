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
	// Use this for initialization
	void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(  Input.GetKey(KeyCode.RightArrow) )
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(WalkForce, 0), ForceMode2D.Impulse);
        }

        if( Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(new Vector2(-WalkForce, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
	}
}
