using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    //Fields
    Rigidbody2D rb;
    bool OnGround = false;
    bool PunchOver = false;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];

    [SerializeField]
    CircleCollider2D castCollider;

    [SerializeField]
    private BoxCollider2D punchCollider;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if( collision.gameObject.tag == "Ground")
        {
            OnGround = true;
            PunchOver = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        castCollider.Cast(new Vector2(0, -1), raycastHit2s);
        rayHit = raycastHit2s[0];

        if( collision.gameObject.tag == "Ground" && rayHit.fraction < 0.25)
        {
            OnGround = false;
        }
    }
    // Update is called once per frame
    void Update ()
    {
		if( !PunchOver && OnGround && Input.GetMouseButtonDown(0) && !punchCollider.enabled)
        {
            punchCollider.enabled = true; 
        }
      
        if(!PunchOver && OnGround && punchCollider.enabled )
        {
            if (punchCollider.offset.x < 0.5f)
            {
                punchCollider.offset = new Vector2(punchCollider.offset.x + 0.01f, punchCollider.offset.y);
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                PunchOver = true;
            }
        }
        else
        {
            punchCollider.enabled = false;
            punchCollider.offset = new Vector2(0, punchCollider.offset.y);
        }
        
    }

}
