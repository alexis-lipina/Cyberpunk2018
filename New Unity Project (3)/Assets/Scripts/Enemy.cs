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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (player == null)
            {
                player = collision.transform;
            }

            RaycastHit2D cast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.33f), new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y));

            if (cast.collider.tag == "Player")
            {
                playerSighted = true;
                controller.SetBool("PlayerSighted", true);
            }
            else
            {
                playerSighted = false;
                controller.SetBool("PlayerSighted", false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerSighted = false;
            controller.SetBool("PlayerSighted", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        controller.SetFloat("SpeedMultiplier", 1);

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
                RaycastHit2D cast = Physics2D.Raycast(new Vector2(transform.position.x + (0.75f * direction.x / distance), transform.position.y), Vector2.down);

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
                    if (distance < minAttackRange)
                    {
                        RaycastHit2D cast = Physics2D.Raycast(new Vector2((transform.position.x + (-0.75f * direction.x / distance)), transform.position.y), Vector2.down);

                        if (cast.distance < 1)
                        {
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
            RaycastHit2D[] casts = new RaycastHit2D[] 
                {
                Physics2D.Raycast(new Vector2(transform.position.x + (0.75f * patrolDirection), transform.position.y), Vector2.down),
                Physics2D.Raycast(new Vector2(transform.position.x + (0.75f * patrolDirection), transform.position.y), new Vector2(patrolDirection, 0))
                };

            if (patrolDirection >= 0)
            {
                renderer.flipX = false;
            }
            else
            {
                renderer.flipX = true;
            }

            if (casts[0].distance > 1 || casts[1].distance < 0.5f)
            {
                patrolDirection *= -1;
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
