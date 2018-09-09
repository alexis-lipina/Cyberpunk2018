using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int shotVelocity;

    /// <summary>
    /// Shoots this projectile
    /// </summary>
    /// <param name="direction">1 for going right -1 for going left</param>
    public void Shoot(int direction)
    {
        rb.velocity = new Vector3(shotVelocity * direction, 0);
    }

    /// <summary>
    /// Destorys this projectile when it hits something
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localPosition = Vector3.zero;
        transform.parent.GetComponent<BasicProjectile>().ProjectilePool.Add(this);

        gameObject.SetActive(false);
    }
}
