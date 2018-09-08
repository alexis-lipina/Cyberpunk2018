using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSuperPunch : Implant
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("SOOPER PUNCH!");
        }
    }
}
