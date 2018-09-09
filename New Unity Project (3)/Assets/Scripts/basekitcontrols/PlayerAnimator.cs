using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator bodyAnimator;
    private Animator legsAnimator;
    private Vector2 movement;

    public Vector2 MoveVector { set { movement = value; } }

    public void Punch()
    {
        bodyAnimator.SetTrigger("Punch");
    }

    public void Shoot()
    {
        bodyAnimator.SetTrigger("Shoot");
    }

    // Use this for initialization
    void Start () {
        bodyAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        legsAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        movement = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        bodyAnimator.SetFloat("xVelocity", movement.x);
        bodyAnimator.SetFloat("yVelocity", movement.y);
        legsAnimator.SetFloat("xVelocity", movement.x);
        legsAnimator.SetFloat("yVelocity", movement.y);
    }
}
