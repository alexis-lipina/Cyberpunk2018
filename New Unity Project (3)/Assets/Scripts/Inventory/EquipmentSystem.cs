using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] private Implant fakeJump;
    [SerializeField] private Implant fakePunch;
    [SerializeField] private Implant fakeSuperPunch;

    //[SerializeField] private List<Implant>

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            DisableImplant(fakePunch);
            Debug.Log("Component disabled");
            Debug.Log("");
        }
	}

    private void DisableImplant(Implant implant)
    {
        implant.enabled = false;
    }
}
