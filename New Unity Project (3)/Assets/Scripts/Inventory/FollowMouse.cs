using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private bool following = false;

    /// <summary>
    /// Gets or sets the the following state of the object
    /// </summary>
    public bool Following { get { return following; } set { following = value; } }

	// Update is called once per frame
	void Update ()
    {
        //if enabled the element will follow the mouse
		if(following)
        {
            transform.position = Input.mousePosition;
        }
	}
}
