using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentSystem : MonoBehaviour
{
    /// <summary>
    /// Disables an implant
    /// </summary>
    /// <param name="implant">Name of implant to disable</param>
    public void DisableImplant(string implant)
    {
        ((Behaviour)transform.GetComponent(Type.GetType(implant))).enabled = false;
    }

    /// <summary>
    /// Enables and implant
    /// </summary>
    /// <param name="implant">Name of implant to enable</param>
    public void ActivateImplant(string implant)
    {
        ((Behaviour)transform.GetComponent(Type.GetType(implant))).enabled = true;
    }
}
