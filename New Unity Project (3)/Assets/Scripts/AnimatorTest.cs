using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour {

    private Animator animator;
    private bool grounded = true;
    private Vector2 movement;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        movement = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grounded = !grounded;
            animator.SetBool("grounded", grounded);
            Debug.Log("JUMP?");
        }
        movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1.0f;
        }
        animator.SetFloat("xVelocity", movement.x);
        animator.SetFloat("yVelocity", movement.y);
    }
}
