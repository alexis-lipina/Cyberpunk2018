using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class AnimatorTest : MonoBehaviour {

    private PlayerAnimator pa;
    private Vector2 movement;

	// Use this for initialization
	void Awake () {
        pa = GetComponent<PlayerAnimator>();
        movement = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pa.Punch();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            pa.Shoot();
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
        pa.MoveVector = movement;
    }
}
