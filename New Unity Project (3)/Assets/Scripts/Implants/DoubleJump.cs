using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Implant
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;

    private bool jumpOneReady = false;
    private bool jumpTwoReady = false;


    private void Start()
    {
        IncompatibleTypes.Add(typeof(Jump));
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.timeScale == 0) { return; }

        if (Input.GetKeyDown(KeyCode.Space) && (jumpOneReady || jumpTwoReady))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            if (jumpOneReady) { jumpOneReady = false; }
            else { jumpTwoReady = false; }
        }
	}

    /// <summary>
    /// Runs when the player collides with a platform
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            jumpOneReady = true;
            jumpTwoReady = true;
        }
    }
}
