using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator bodyAnimator; //ref to animator component in body
    private Animator legsAnimator; //ref to animator component in legs
    private Vector2 movement; //move Velocity of the player

    /// <summary>
    /// set the moveVector to the players velocity
    /// </summary>
    public Vector2 MoveVector { set { movement = value; } }

    // Use this for initialization
    void Start () {
        bodyAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        legsAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        movement = new Vector2(0, 0);
    }
	
    /// <summary>
    /// updates the x and y velocities in animators to match player
    /// </summary>
	void Update () {
        bodyAnimator.SetFloat("xVelocity", movement.x);
        bodyAnimator.SetFloat("yVelocity", movement.y);
        legsAnimator.SetFloat("xVelocity", movement.x);
        legsAnimator.SetFloat("yVelocity", movement.y);
    }

    /// <summary>
    /// animates punching
    /// </summary>
    public void Punch()
    {
        bodyAnimator.SetTrigger("Punch");
    }

    /// <summary>
    /// animates shooting
    /// </summary>
    public void Shoot()
    {
        bodyAnimator.SetTrigger("Shoot");
    }
}
