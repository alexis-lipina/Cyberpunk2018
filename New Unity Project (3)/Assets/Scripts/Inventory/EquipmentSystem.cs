using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentSystem : MonoBehaviour
{
    //[SerializeField] private List<Implant>

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            //DisableImplant(fakePunch);
            //Debug.Log("Component disabled");
            //Debug.Log("");
        }
	}

    public void DisableImplant(string implant)
    {
        ((Behaviour)transform.GetComponent(Type.GetType(implant))).enabled = false;
    }

    public void ActivateImplant(string implant)
    {
        ((Behaviour)transform.GetComponent(Type.GetType(implant))).enabled = true;
    }
}
