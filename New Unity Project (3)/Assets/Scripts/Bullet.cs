using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    private Vector2 direction;
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Damage the player
        }
        else if (collision.isTrigger == false)
        {
            Destroy(gameObject);
        }
    }
}
