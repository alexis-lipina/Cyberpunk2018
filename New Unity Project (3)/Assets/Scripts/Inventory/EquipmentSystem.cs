using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] Input fakeJump;
    [SerializeField] Input fakePunch;
    [SerializeField] Input fakeSuperPunch;

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Q))
        {

        }
	}

    private void DisableImplant(Implant implant)
    {
        implant.enabled = false;
    }
}
