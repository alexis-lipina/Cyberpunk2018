using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Animator), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{

    private bool playerSighted, inAttackRange;
    [SerializeField, Tooltip("Enemy will walk back and forth instead of idling.")]
    private bool patrol;
    [SerializeField, Tooltip("Enemy will try to stay out of melee range.")]
    private bool ranged;
    [SerializeField, Tooltip("The projectile to be fired.")]
    private GameObject bullet;
    [SerializeField, Tooltip("The range at which the enemy can attack.")]
    private float attackRange;
    [SerializeField, Tooltip("For ranged enemies only, the minimum range at which they will attack")]
    private float minAttackRange;
    [SerializeField, Tooltip("The force with which bullets are fired")]
    private float firingForce;
    [SerializeField]
    private float moveForce;
    private Animator controller;
    private SpriteRenderer renderer;
    private Rigidbody2D rigidBody;
    private Transform player;
    public bool shoot = false;
    //Count the time since the last shot was fired
    private float timePassed = 0;
    private float patrolDirection = 1;

    private void Start()
    {
        controller = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        if (patrol)
        {
            controller.SetBool("Patrol", true);
        }
    }

    //When a player enters the trigger it is sighted I will add raycasting for sight later
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            /*RaycastHit2D[] casts = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 0.33f), new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y));

            foreach(RaycastHit2D cast in casts)
            {
                Debug.Log("VisionRay:\nTag: " + cast.collider.tag + "\nName: " + cast.collider.name);

                if (cast.collider.tag == "Player")
                {*/
                    playerSighted = true;
                    controller.SetBool("PlayerSighted", true);
                /*}
            }*/
        }
    }

    //When a player exits the trigger is is unsighted
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerSighted = false;
            controller.SetBool("PlayerSighted", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (playerSighted)
        {
            Vector3 direction = player.position - transform.position;
            float distance = Mathf.Abs(direction.x);

            if (direction.x >= 0)
            {
                renderer.flipX = false;
            }
            else
            {
                renderer.flipX = true;
            }

            if (distance > attackRange)
            {
                RaycastHit2D cast = Physics2D.RaycastAll(new Vector2(transform.position.x + (0.75f * direction.x / distance), transform.position.y), Vector2.down)[1];

                if(cast.distance < 1)
                {
                    rigidBody.velocity = new Vector2(direction.x / distance, rigidBody.velocity.y) * moveForce;
                    controller.SetBool("Unreachable", false);
                }
                else
                {
                    controller.SetBool("Unreachable", true);
                }
                controller.SetBool("InAttackRange", false);
            }
            else
            {
                if (ranged)
                {
                    controller.SetFloat("SpeedMultiplier", 1);
                    if (distance < minAttackRange)
                    {
                        RaycastHit2D cast = Physics2D.RaycastAll(new Vector2(transform.position.x + (0.75f * direction.x / distance), transform.position.y), Vector2.down)[1];

                        if (cast.distance < 1)
                        {
                            rigidBody.velocity = new Vector2(direction.x / (-1 * distance * 1.33f), rigidBody.velocity.y) * moveForce;
                        }
                        rigidBody.velocity = new Vector2(direction.x / (-1 * distance * 1.33f), rigidBody.velocity.y) * moveForce;
                        controller.SetFloat("SpeedMultiplier", -0.75f);
                        controller.SetBool("InAttackRange", false);
                    }
                    else
                    {
                        controller.SetBool("InAttackRange", true);
                    }
                }
                else
                {
                    controller.SetBool("InAttackRange", true);
                }

                if (controller.GetBool("InAttackRange") && shoot && timePassed >= 0.25f)
                {
                    Shoot(direction);
                    shoot = false;
                }
            }
        }

        if (controller.GetBool("PlayerSighted") == false)
        {
            controller.SetBool("Unreachable", false);

            if (patrol)
            {
                Patrol();
            }
        }
    }

    public void Patrol()
    {
        if (patrol)
        {
            //Check for edge
            RaycastHit2D cast = Physics2D.RaycastAll(new Vector2(transform.position.x + (0.75f * patrolDirection), transform.position.y), Vector2.down)[1];

            if (cast.distance > 1)
            {
                patrolDirection *= -1;

                if (renderer.flipX)
                {
                    renderer.flipX = false;
                }
                else
                {
                    renderer.flipX = true;
                }
            }
        }

        rigidBody.velocity = new Vector2(patrolDirection * moveForce, rigidBody.velocity.y);
    }

    public void Shoot(Vector2 direction)
    {
        direction.Normalize();
        float angle = direction.x / direction.y;

        Bullet newBullet = Instantiate(bullet, transform.position + new Vector3(direction.x * 0.75f, direction.y + 0.25f, 0), Quaternion.Euler(0, 0, angle)).GetComponent<Bullet>();

        newBullet.GetComponent<Rigidbody2D>().velocity = direction * firingForce;

        timePassed = 0;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(new Vector3(GetComponent<CircleCollider2D>().offset.x, GetComponent<CircleCollider2D>().offset.y, 0) + transform.position, GetComponent<CircleCollider2D>().radius);

    //    Gizmos.color = Color.green;

    //    Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.33f, 0), new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y, 0));
    //}
}
