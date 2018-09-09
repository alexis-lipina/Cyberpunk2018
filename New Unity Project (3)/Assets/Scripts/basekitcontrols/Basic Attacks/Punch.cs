using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    //Fields
    Rigidbody2D rb;
    bool PunchOver = false;

    RaycastHit2D rayHit;
    RaycastHit2D[] raycastHit2s = new RaycastHit2D[20];


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
            PunchOver = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground"  && !punchCollider.enabled)
        {
            PunchOver = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if( !PunchOver && Input.GetMouseButtonDown(0) && !punchCollider.enabled)
        {
            punchCollider.enabled = true; 
        }
      
        if(!PunchOver && punchCollider.enabled )
        {
            if( gameObject.GetComponent<HorizontalMovement>().LeftRight == 1)
            {
                if (punchCollider.offset.x < 0.5f)
                {
                    punchCollider.offset = new Vector2(punchCollider.offset.x + 0.01f, punchCollider.offset.y);
                    rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
                    Debug.Log("Pumch");
                }
                else
                {
                    PunchOver = true;
                }
            }
            else
            {
                if (punchCollider.offset.x > -0.5f)
                {
                    punchCollider.offset = new Vector2(punchCollider.offset.x - 0.01f, punchCollider.offset.y);
                    rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
                    Debug.Log("Pumch");
                }
                else
                {
                    PunchOver = true;
                }
            }
            
        }
        else
        {
            punchCollider.enabled = false;
            punchCollider.offset = new Vector2(0, punchCollider.offset.y);
        }
        
    }

}
