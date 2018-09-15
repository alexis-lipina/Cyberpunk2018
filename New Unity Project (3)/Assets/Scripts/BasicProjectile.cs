using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Implant
{
    private List<System.Type> incompatibleTypes = new List<System.Type>();

    [SerializeField] private List<Projectile> projectilePool;

    public List<Projectile> ProjectilePool { get { return projectilePool; } }

    public override List<Type> IncompatibleTypes
    {
        get
        {
            return incompatibleTypes;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //shoots the bullet when you click
		if(Input.GetMouseButtonDown(1))
        {
            Projectile projectile = projectilePool[projectilePool.Count - 1];
            projectilePool.RemoveAt(projectilePool.Count - 1);

            projectile.gameObject.SetActive(true);

            projectile.transform.localPosition = new Vector3(.2f * transform.GetComponent<HorizontalMovement>().LeftRight, 0);

            projectile.Shoot(1 * transform.GetComponent<HorizontalMovement>().LeftRight);

            gameObject.GetComponent<PlayerAnimator>().Shoot();
        }
	}
}
