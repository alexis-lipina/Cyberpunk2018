using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentSystem : MonoBehaviour
{
    private List<Implant> enabledImplants = new List<Implant>() { };

    /// <summary>
    /// Disables an implant
    /// </summary>
    /// <param name="implant">Name of implant to disable</param>
    public void DisableImplant(string implant)
    {
        //get a reference to the component to disable
        Behaviour toDisable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        //ensure base implants overriden by the disabled implant are re enabled after it is removed
        foreach(Type type in ((Implant)toDisable).IncompatibleTypes)
        {
            //change to Jump
            if(type == typeof(DoubleJump))
            {
                //enable Jump
            }
            //etc...
        }

        //remove from the list of enabled implants
        enabledImplants.Remove((Implant)toDisable);

        //disable the implant
        toDisable.enabled = false;
    }

    /// <summary>
    /// Enables and implant
    /// </summary>
    /// <param name="implant">Name of implant to enable</param>
    public void ActivateImplant(string implant)
    {
        //get a reference to the component to enable
        Behaviour toEnable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        foreach(Type type in ((Implant)toEnable).IncompatibleTypes)
        {
            for(int i = 0; i < enabledImplants.Count; i++)
            {
                if(enabledImplants[i].IncompatibleTypes.Contains(type))
                {
                    enabledImplants[i].enabled = false;
                    enabledImplants.RemoveAt(i);
                    i--;
                }
            }
        }

        //add to the lis tof enabled implants
        enabledImplants.Add((Implant)toEnable);

        //enable the implant
        toEnable.enabled = true;
    }
}
