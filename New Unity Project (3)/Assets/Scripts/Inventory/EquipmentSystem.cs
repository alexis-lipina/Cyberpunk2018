using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentSystem : MonoBehaviour
{
    //equipped implants
    private Implant equipmentSlot1;
    private Implant equipmentSlot2;
    private Implant equipmentSlot3;

    [SerializeField] Implant jump;
    [SerializeField] Implant horizontal;

    /// <summary>
    /// Disables an implant
    /// </summary>
    /// <param name="implant">Name of implant to disable</param>
    public void DisableImplant(string implant, int equipmentSlot)
    {
        //get a reference to the component to disable
        Behaviour toDisable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        //ensure base implants overriden by the disabled implant are re enabled after it is removed
        foreach (Type type in ((Implant)toDisable).IncompatibleTypes)
        {
            if (type == typeof(Jump))
            {
                jump.enabled = true;
            }
            //etc...
        }

        //remove from the list of enabled implants
        switch(equipmentSlot)
        {
            case 1:
                equipmentSlot1 = null;
                break;
            case 2:
                equipmentSlot2 = null;
                break;
            case 3:
                equipmentSlot3 = null;
                break;
        }

        //disable the implant
        toDisable.enabled = false;
    }

    /// <summary>
    /// Enables and implant
    /// </summary>
    /// <param name="implant">Name of implant to enable</param>
    public void ActivateImplant(string implant, int equipmentSlot)
    {
        //get a reference to the component to enable
        Behaviour toEnable = ((Behaviour)transform.GetComponent(Type.GetType(implant)));

        foreach (Type type in ((Implant)toEnable).IncompatibleTypes)
        {
            //for(int i = 0; i < enabledImplants.Count; i++)
            //{
            //    if(enabledImplants[i].IncompatibleTypes.Contains(type))
            //    {
            //        enabledImplants[i].enabled = false;
            //        enabledImplants.RemoveAt(i);
            //        i--;
            //    }
            //}
            switch (equipmentSlot)
            {
                case 1:
                    if(equipmentSlot2.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot2 = null;
                    }
                    if(equipmentSlot3.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot3 = null;
                    }
                    break;

                case 2:
                    if(equipmentSlot1.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot1 = null;
                    }
                    if(equipmentSlot3.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot3 = null;
                    }
                    break;
                case 3:
                    if(equipmentSlot1.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot1 = null;
                    }
                    if(equipmentSlot2.IncompatibleTypes.Contains(type))
                    {
                        equipmentSlot2 = null;
                    }
                    break;
            }
        }

        //add to the list of enabled implants
        //enabledImplants.Add((Implant)toEnable);

        //enable the implant
        toEnable.enabled = true;
    }
}
